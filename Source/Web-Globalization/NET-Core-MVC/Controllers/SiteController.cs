using System.Threading.Tasks;
using Company.WebApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Company.WebApplication.Controllers
{
	public abstract class SiteController : Controller
	{
		#region Methods

		protected internal virtual IViewModel CreateViewModel()
		{
			return new ViewModel(this.HttpContext);
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public virtual async Task<IActionResult> Index()
		{
			return this.View(this.CreateViewModel());
		}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

		#endregion
	}
}