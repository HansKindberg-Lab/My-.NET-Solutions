using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Company.WebApplication
{
	public class Global : HttpApplication
	{
		#region Methods

		protected void Application_Start(object sender, EventArgs e)
		{
			AreaRegistration.RegisterAllAreas();
			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			RouteTable.Routes.MapRoute("Default", "{controller}/{action}/{id}", new {controller = "Home", action = "Index", id = UrlParameter.Optional});

			BundleTable.EnableOptimizations = bool.Parse(ConfigurationManager.AppSettings["EnableBundleOptimizations"]);
			BundleTable.Bundles.FileExtensionReplacementList.Clear();

			var scriptBundle = new ScriptBundle("~/Site-scripts").Include(BundleTable.EnableOptimizations ? new[] {"~/Scripts/Site.min.js"} : new[] {"~/Scripts/jquery.js", "~/Scripts/popper.js", "~/Scripts/bootstrap.js", "~/Scripts/Main.js"});
			scriptBundle.Transforms.Clear();
			BundleTable.Bundles.Add(scriptBundle);

			var styleBundle = new StyleBundle("~/Site-style").Include(BundleTable.EnableOptimizations ? new[] {"~/Style/Site.min.css"} : new[] {"~/Style/bootstrap.css", "~/Style/Main.css"});
			styleBundle.Transforms.Clear();
			BundleTable.Bundles.Add(styleBundle);
		}

		#endregion
	}
}