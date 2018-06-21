using Microsoft.AspNetCore.Mvc;

namespace Company.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		#region Methods

		public virtual IActionResult Index()
		{
			return this.View();
		}

		#endregion
	}
}