using System.Security.Cryptography.X509Certificates;
using Company.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.WebApplication.Controllers
{
	public abstract class SiteController : Controller
	{
		#region Methods

		protected internal virtual IActionResult CreateDefaultView()
		{
			return this.CreateDefaultView(null);
		}

		protected internal virtual IActionResult CreateDefaultView(X509Certificate2 clientCertificate)
		{
			return this.View("~/Views/Home/Index.cshtml", new ViewModel {ClientCertificate = clientCertificate});
		}

		#endregion
	}
}