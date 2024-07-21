using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DatabasePN;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhoneNoteApplication.Data;
using PhoneNoteApplication.Models;
using PhoneNotes.Models;
using ClaimTypes = System.Security.Claims.ClaimTypes;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace PhoneNotes.Controllers;

public sealed class ApiAccountController : Controller
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly DatabaseContext _database;
	private RoleManager<Role> _roleManager;
	private readonly IConfiguration _configuration;

	public ApiAccountController(UserManager<User> userManager, SignInManager<User> signInManager,
								DatabaseContext database, RoleManager<Role> roleManager, IConfiguration configuration)
	{
		_roleManager = roleManager;
		_configuration = configuration;
		_userManager = userManager;
		_signInManager = signInManager;
		_database = database;
	}

	// [HttpPost]
	// public async Task<IActionResult> Login([FromBody] UserLoginModel model)
	// {
	// 	if (!ModelState.IsValid)
	// 	{
	// 		ModelState.AddModelError(string.Empty, "User is not found");
	//
	// 		return BadRequest(ModelState);
	// 	}
	//
	// 	var loginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
	//
	// 	if (!loginResult.Succeeded)
	// 	{
	// 		return BadRequest();
	// 	}
	//
	// 	UserDto? userWithRolesAndClaims = await GetUserWithRolesAndClaims(model);
	//
	// 	return new JsonResult(userWithRolesAndClaims);
	// }
	
	public async Task<IActionResult> Login([FromBody] UserLoginModel model)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var loginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

		if (!loginResult.Succeeded)
		{
			return Unauthorized();
		}

		var user = await _userManager.FindByNameAsync(model.UserName);
		var roles = await _userManager.GetRolesAsync(user);

		var claims = new List<Claim>
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
		};

		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _configuration["JwtSettings:Issuer"],
			audience: _configuration["JwtSettings:Audience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(30),
			signingCredentials: creds);

		return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
	}

	[HttpPost]
	public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		User user = new() { UserName = model.UserName };
		var createResult = await _userManager.CreateAsync(user, model.Password);
		await _userManager.AddToRoleAsync(user, Roles.User);

		if (createResult.Succeeded)
		{
			await _signInManager.SignInAsync(user, false);

			return Ok();
		}

		foreach (IdentityError error in createResult.Errors)
		{
			ModelState.AddModelError(string.Empty, error.Description);
		}

		return BadRequest(ModelState);
	}

	private async Task<UserDto?> GetUserWithRolesAndClaims(UserLoginModel model)
	{
		User? user = await _database.Users.FirstOrDefaultAsync(user => user.UserName == model.UserName);

		if (user == null)
		{
			return null;
		}

		List<RoleDto> roles = new();
		IList<string> roleNames = await _userManager.GetRolesAsync(user);

		foreach (string roleName in roleNames)
		{
			Role? role = await _database.Roles.FirstOrDefaultAsync(role => role.Name == roleName);

			if (role == null)
			{
				continue;
			}

			IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(role);
			IList<ClaimDto> roleClaimDtos = CreateClaims(roleClaims);


			RoleDto roleDto = new() { Name = role.Name, Claims = roleClaimDtos };
			roles.Add(roleDto);
		}

		IList<Claim> userClains = await _userManager.GetClaimsAsync(user);
		IList<ClaimDto> userClaimDtos = CreateClaims(userClains);

		UserDto userDto = new()
		{
			UserName = user.UserName,
			Email = user.Email,
			PhoneNumber = user.PhoneNumber,
			Roles = roles,
			Claims = userClaimDtos
		};


		return userDto;
	}

	private IList<ClaimDto> CreateClaims(IList<Claim> claims)
	{
		IList<ClaimDto> claimDtos = new List<ClaimDto>();

		foreach (Claim claim in claims)
		{
			ClaimDto claimDto = new()
			{
				Type = claim.ValueType, 
				Value = claim.Value,
			};
			
			claimDtos.Add(claimDto);
		}

		return claimDtos;
	}
}
