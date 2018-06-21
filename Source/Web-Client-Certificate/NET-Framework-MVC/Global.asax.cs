using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Company.WebApplication.Controllers;

namespace Company.WebApplication
{
	public class Global : HttpApplication
	{
		#region Methods

		protected internal virtual void Application_Error(object sender, EventArgs e)
		{
			var routeData = new RouteData();
			routeData.Values["controller"] = "Exception";
			routeData.Values["action"] = "Index";

			((IController) new ExceptionController(this.Server.GetLastError())).Execute(new RequestContext(new HttpContextWrapper(this.Context), routeData));
		}

		protected internal virtual void Application_Start(object sender, EventArgs e)
		{
			AreaRegistration.RegisterAllAreas();
			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			RouteTable.Routes.MapRoute("Default", "{controller}/{action}/{id}", new {controller = "Home", action = "Index", id = UrlParameter.Optional});
		}

		#endregion
	}
}