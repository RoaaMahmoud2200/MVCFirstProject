using Company.Route2.DAL.Models;
using Company.Route2.PL.Helper.EmailSetting;
using Company.Route2.PL.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Common;

namespace Company.Route2.PL.Controllers
{
    

    public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _SignInManager)
		{
			this._userManager = _userManager;
			this._signInManager = _SignInManager;
		}


		public IActionResult AccessDenied()
		{
			return View();
		}
		[HttpGet]
		//sign Up
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{

					var user = await _userManager.FindByNameAsync(model.UserName);
					if (user is null)
					{
						user = await _userManager.FindByEmailAsync(model.Email);
						if (user is null)
						{
							var MappingModel = new ApplicationUser()
							{
								UserName = model.UserName,
								Fname = model.Fname,
								Lname = model.Lname,
								Email = model.Email,
								IsAgree = model.IsAgree
							};
							var result = await _userManager.CreateAsync(MappingModel, model.Password);
							if (result.Succeeded)
							{
								return RedirectToAction("SignIn");
							}
							foreach (var err in result.Errors)
							{
								ModelState.AddModelError(string.Empty, err.Description);
							}
						}
						ModelState.AddModelError(string.Empty, "this email is already used");
						return View(model);
					}
					ModelState.AddModelError(string.Empty, "this username is already used");

				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}

			}
			return View(model);
		}



		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						var flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (flag)
						{
							//generate token 
							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
							if (result.Succeeded)
							{
								return RedirectToAction("Index", "Home");
							}
						}

						ModelState.AddModelError(string.Empty, "InvalidLogin !!");
						return View(model);
					}
					ModelState.AddModelError(string.Empty, "InvalidLogin !!");

				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}

			return View(model);
		}

		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn");
		}


		public IActionResult ForgetPassword()
		{
			return View();
		}
		public async Task<IActionResult> SendResetPasswordURL(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);

					if (user is not null)
					{
						var token = await _userManager.GeneratePasswordResetTokenAsync(user);
						var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);
						Email email = new Email()
						{
							to = model.Email,
							subject = "reset password",
							body = url
						};
						EmailSetting.SendEmail(email);
						return RedirectToAction("CheckYourInbox");
					}
					ModelState.AddModelError(string.Empty, " Enter valid Email ");

				} catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}
		
		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);

				if (user is not null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded)
					{
						return RedirectToAction("SignIn");
					}
					//foreach (var item in result.Errors)
					//{
					//	ModelState.AddModelError(string.Empty, item.Description);

					//}
				}

			}
			return View(model);
		}
	} 
}
