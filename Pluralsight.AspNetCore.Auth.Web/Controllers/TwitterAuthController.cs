using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Pluralsight.AspNetCore.Auth.Web.Controllers
{
	[Route("twitterauth")]
	public class TwitterAuthController : Controller
	{
		[Route("signin")]
		public IActionResult SignIn()
		{
			return View();
		}

		[Route("signin/{provider}")]
		public IActionResult SignIn(string provider, string returnUrl = null)
		{
			return Challenge(new AuthenticationProperties { RedirectUri = returnUrl ?? "/" }, provider);
		}

		[Route("signout")]
		[HttpPost]
		public async Task<IActionResult> SignOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
	}
}