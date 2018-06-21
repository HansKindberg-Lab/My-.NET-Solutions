using System.Web;
using System.Web.Mvc;
using Company.WebApplication.Models;

namespace Company.WebApplication.Controllers
{
	public abstract class SiteController : Controller
	{
		#region Methods

		protected internal virtual ActionResult CreateDefaultView()
		{
			return this.CreateDefaultView(null);
		}

		protected internal virtual ActionResult CreateDefaultView(HttpClientCertificate clientCertificate)
		{
			if(clientCertificate != null && !clientCertificate.IsPresent)
				clientCertificate = null;

			return this.View("~/Views/Home/Index.cshtml", new ViewModel {ClientCertificate = clientCertificate});
		}

		#endregion
	}
}