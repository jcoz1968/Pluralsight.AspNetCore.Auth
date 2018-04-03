using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Pluralsight.AspNetCore.Auth.Web.Controllers
{
	[Route("facebookauth")]
	public class FacebookAuthController : Controller
	{
		[Route("signin")]
		public IActionResult SignIn()
		{
			return Challenge(new AuthenticationProperties { RedirectUri = "/" });
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