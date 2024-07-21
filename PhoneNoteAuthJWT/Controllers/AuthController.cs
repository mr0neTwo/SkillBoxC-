using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatabasePN;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhoneNoteApplication.Models;
using PhoneNoteAuthJWT.Models;

namespace PhoneNoteAuthJWT.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly DatabaseContext _context;
	private readonly IConfiguration _config;

	public AuthController(DatabaseContext context, IConfiguration config)
	{
		_context = context;
		_config = config;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] UserRegisterModel userRegisterModel)
	{
		if (await _context.Users.AnyAsync(user => user.UserName == userRegisterModel.UserName))
		{
			return Conflict("UserName already exists");
		}

		User user = new()
		{
			UserName = userRegisterModel.UserName,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterModel.Password),
			Email = userRegisterModel.Email
		};

		_context.Users.Add(user);
		await _context.SaveChangesAsync();

		return Ok();
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] UserLoginModel userLoginModel)
	{
		User? user = await _context.Users.SingleOrDefaultAsync(user => user.UserName == userLoginModel.UserName);

		if (user == null || !BCrypt.Net.BCrypt.Verify(userLoginModel.Password, user.PasswordHash))
		{
			return Unauthorized();
		}

		LoginResponse loginResponse = new() { Token = GenerateToken(user) };

		return Ok(loginResponse);
	}

	private string GenerateToken(User user)
	{
		SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
		SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

		Claim[] claims =
		{
			new(JwtRegisteredClaimNames.Sub, user.UserName),
			new(JwtRegisteredClaimNames.Email, user.Email),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		JwtSecurityToken token = new
		(
			claims : claims, 
			expires : DateTime.UtcNow.AddHours(48),
			signingCredentials : credentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
