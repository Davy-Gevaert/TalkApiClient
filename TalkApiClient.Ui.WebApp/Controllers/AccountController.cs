using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TalkApiClient.Ui.WebApp.Models;

namespace TalkApiClient.Ui.WebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

		public AccountController(
			SignInManager<IdentityUser> signInManager,
			UserManager<IdentityUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public async Task<IActionResult> SignIn(string returnUrl)
		{
			// Clear the existing external cookie to ensure a clean login process
			await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

			var model = new SignInModel { ReturnUrl = returnUrl };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInModel model)
		{
			if (ModelState.IsValid)
			{
				// This doesn't count login failures towards account lockout
				// To enable password failures to trigger account lockout, set lockoutOnFailure: true
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					return RedirectToReturnUrl(model.ReturnUrl);
				}

				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Register(string returnUrl)
		{
			var model = new RegisterModel { ReturnUrl = returnUrl };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new IdentityUser { UserName = model.Email, Email = model.Email };
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
					return RedirectToReturnUrl(model.ReturnUrl);
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> SignOut(string returnUrl)
		{
			await _signInManager.SignOutAsync();
			if (returnUrl != null)
			{
				return RedirectToReturnUrl(returnUrl);
			}

			return RedirectToAction("Index", "Home");
		}

		private IActionResult RedirectToReturnUrl(string returnUrl)
		{
			if (string.IsNullOrWhiteSpace(returnUrl))
			{
				returnUrl = "/";
			}

			return LocalRedirect(returnUrl);
		}

		public IActionResult AccessDenied(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
	}
}
