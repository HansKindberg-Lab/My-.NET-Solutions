using System.Web.Mvc;

namespace Company.WebApplication.Controllers
{
	public class HomeController : SiteController
	{
		#region Methods

		public virtual ActionResult Index()
		{
			return this.CreateDefaultView();
		}

		#endregion
	}
}