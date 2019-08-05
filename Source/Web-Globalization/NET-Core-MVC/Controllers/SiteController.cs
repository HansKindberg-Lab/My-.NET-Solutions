using System;
using System.Globalization;
using System.Threading.Tasks;
using Company.WebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Company.WebApplication.Controllers
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
	public abstract class SiteController : Controller
	{
		#region Methods

		protected internal virtual IViewModel CreateViewModel()
		{
			return new ViewModel(this.HttpContext);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual async Task<IActionResult> DeleteCookie(Uri returnUrl)
		{
			this.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);

			return await this.Redirect(returnUrl);
		}

		public virtual async Task<IActionResult> Index()
		{
			return this.View(this.CreateViewModel());
		}

		protected internal virtual async Task<IActionResult> Redirect(Uri returnUrl)
		{
			if(returnUrl == null || returnUrl.IsAbsoluteUri)
				return this.Redirect("/");

			return this.Redirect(returnUrl.ToString());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual async Task<IActionResult> SaveCookie(CultureInfo culture, Uri returnUrl, CultureInfo uiCulture)
		{
			this.HttpContext.Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, uiCulture)),
				new CookieOptions {MaxAge = TimeSpan.FromDays(365)}
			);

			return await this.Redirect(returnUrl);
		}

		#endregion
	}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}