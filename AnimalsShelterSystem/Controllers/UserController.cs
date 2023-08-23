using AnimalsShelterSystem.Data.Models;
using AnimalsShelterSystem.Web.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalsShelterSystem.Web.Controllers
{
	public class UserController : Controller
	{
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IUserStore<ApplicationUser> userStore;
		private readonly IMemoryCache memoryCache;

		public UserController(SignInManager<ApplicationUser> signInManager,
							  UserManager<ApplicationUser> userManager,
							  IMemoryCache memoryCache,
							  IUserStore<ApplicationUser> userStore)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;

			this.memoryCache = memoryCache;
			this.userStore = userStore;
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register(RegisterFormViewModel model)
		{

			//var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
			if (ModelState.IsValid)
			{
				var user = CreateUser();
				await userManager.SetEmailAsync(user, model.Email);

				await userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
				var result = await userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					//var userId = await _userManager.GetUserIdAsync(user);
					//if (_userManager.Options.SignIn.RequireConfirmedAccount)
					//{
					//    return RedirectToPage("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl });
					//}

					await signInManager.SignInAsync(user, isPersistent: false);
					//if (this.User.IsAdmin())
					//{
					//	return Redirect("Admin/Products/All");
					//}
					return Redirect("/Home/Index");

				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);

		}
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginFormViewModel model)
		{


			//ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

			if (ModelState.IsValid)
			{
				// This doesn't count login failures towards account lockout
				// To enable password failures to trigger account lockout, set lockoutOnFailure: true
				var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					//_logger.LogInformation("User logged in.");

					return Redirect("/Home/Index");
				}
				if (result.RequiresTwoFactor)
				{
					return RedirectToPage("./LoginWith2fa", new { model.RememberMe });
				}
				if (result.IsLockedOut)
				{
					//_logger.LogWarning("User account locked out.");
					return RedirectToPage("./Lockout");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return View(model);
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}



		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			//_logger.LogInformation("User logged out.");

			// This needs to be a redirect so that the browser performs a new
			// request and the identity for the user gets updated.
			return RedirectToAction("Index", "Home");

		}

		private ApplicationUser CreateUser()
		{
			try
			{
				return Activator.CreateInstance<ApplicationUser>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
					$"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
					$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
			}
		}

		private IUserEmailStore<ApplicationUser> GetEmailStore()
		{
			if (!userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("The default UI requires a user store with email support.");
			}
			return (IUserEmailStore<ApplicationUser>)userStore;
		}

	}
}
