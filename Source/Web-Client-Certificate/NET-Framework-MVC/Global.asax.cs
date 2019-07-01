using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Company.WebApplication
{
	public class Global : HttpApplication
	{
		#region Methods

		protected internal virtual void Application_Start(object sender, EventArgs e)
		{
			AreaRegistration.RegisterAllAreas();
			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			RouteTable.Routes.MapRoute("Default", "{controller}/{action}/{id}", new {controller = "Home", action = "Index", id = UrlParameter.Optional});
		}

		#endregion
	}
}