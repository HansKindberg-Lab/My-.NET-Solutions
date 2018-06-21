using Microsoft.AspNetCore.Mvc;

namespace Company.WebApplication.Controllers
{
	public class HomeController : SiteController
	{
		#region Methods

		public virtual IActionResult Index()
		{
			return this.CreateDefaultView();
		}

		#endregion
	}
}