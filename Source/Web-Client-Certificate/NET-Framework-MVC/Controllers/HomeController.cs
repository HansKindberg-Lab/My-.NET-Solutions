using System.Web.Mvc;

namespace Company.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		#region Methods

		public virtual ActionResult Index()
		{
			return this.View("Index");
		}

		public virtual ActionResult NegotiateClientCertificate()
		{
			return this.Index();
		}

		public virtual ActionResult RequireClientCertificate()
		{
			return this.Index();
		}

		public virtual ActionResult SslNegotiateClientCertificate()
		{
			return this.Index();
		}

		public virtual ActionResult SslRequireClientCertificate()
		{
			return this.Index();
		}

		#endregion
	}
}