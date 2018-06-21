using Microsoft.AspNetCore.Mvc;

namespace Company.WebApplication.Controllers
{
	public class AskForClientCertificateController : SiteController
	{
		#region Methods

		public virtual IActionResult Index()
		{
			return this.CreateDefaultView(this.HttpContext.Connection.ClientCertificate);
		}

		#endregion
	}
}