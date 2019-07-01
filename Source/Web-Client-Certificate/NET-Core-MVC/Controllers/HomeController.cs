using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Company.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		#region Methods

#pragma warning disable 1998
		public virtual async Task<IActionResult> Index()
		{
			return this.View("Index");
		}
#pragma warning restore 1998

		public virtual async Task<IActionResult> NegotiateClientCertificate()
		{
			return await this.Index();
		}

		public virtual async Task<IActionResult> RequireClientCertificate()
		{
			return await this.Index();
		}

		public virtual async Task<IActionResult> SslNegotiateClientCertificate()
		{
			return await this.Index();
		}

		public virtual async Task<IActionResult> SslRequireClientCertificate()
		{
			return await this.Index();
		}

		#endregion
	}
}