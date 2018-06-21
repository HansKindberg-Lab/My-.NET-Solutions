using System.Web.Mvc;

namespace Company.WebApplication.Controllers
{
	public class AskForClientCertificateController : SiteController
	{
		#region Methods

		public virtual ActionResult Index()
		{
			return this.CreateDefaultView(this.Request.ClientCertificate);
		}

		#endregion
	}
}