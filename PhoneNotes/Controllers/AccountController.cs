using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneNoteApplication.Models;
using PhoneNotes.Models;

namespace PhoneNotes.Controllers;

public sealed class AccountController : Controller
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	[HttpGet]
	public IActionResult Login(string returnUrl)
	{
		UserLoginModel userLogin = new() { ReturnUrl = returnUrl };

		return View(userLogin);
	}

	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(UserLoginModel model)
	{
		if (!ModelState.IsValid)
		{
			ModelState.AddModelError(string.Empty, "User is not found");

			return View(model);
		}
		
		var loginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

		if (!loginResult.Succeeded)
		{
			return View(model);
		}

		if (Url.IsLocalUrl(model.ReturnUrl))
		{
			return Redirect(model.ReturnUrl);
		}

		return RedirectToAction("List", "Note");
	}

	[HttpGet]
	public IActionResult Register()
	{
		UserRegisterModel model = new();

		return View(model);
	}
	
	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(UserRegisterModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}

		User user = new() { UserName = model.UserName };
		var createResult = await _userManager.CreateAsync(user, model.Password);

		if (createResult.Succeeded)
		{
			await _signInManager.SignInAsync(user, false);

			return RedirectToAction("List", "Note");
		}

		foreach (IdentityError error in createResult.Errors)
		{
			ModelState.AddModelError(string.Empty, error.Description);
		}
		
		return View(model);
	}

	[HttpPost, ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();

		return RedirectToAction("Login", "Account");
	}
}