using System;
using Company.WebApplication.Business.DependencyInjection.Extensions;
using Company.WebApplication.Business.Web.Mvc.Configuration;
using Company.WebApplication.Business.Web.Mvc.Filters;
using Company.WebApplication.Business.Web.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.WebApplication
{
	public class Startup
	{
		#region Constructors

		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		#endregion

		#region Properties

		public virtual IConfiguration Configuration { get; }

		#endregion

		#region Methods

		public virtual void Configure(IApplicationBuilder applicationBuilder)
		{
			if(applicationBuilder == null)
				throw new ArgumentNullException(nameof(applicationBuilder));

			applicationBuilder.UseDeveloperExceptionPage();
			applicationBuilder.UseStaticFiles();
			applicationBuilder.UseRequestLocalization();
			applicationBuilder.UseMvc(routeBuilder =>
			{
				var certificateControllerParameter = new {controller = "Certificate"};
				var notCertificateControllerConstraints = new {controller = "((?!Certificate).)*"}; // Another expression could be: "First|Home|Second|Third"

				routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");

				routeBuilder.MapRoute("Default-Localized", "{culture:culture}/{ui-culture:ui-culture}/{controller=Home}/{action=Index}/{id?}", null, notCertificateControllerConstraints);
				routeBuilder.MapRoute("Default-Localized-Culture", "{culture:culture}/{controller=Home}/{action=Index}/{id?}", null, notCertificateControllerConstraints);
				routeBuilder.MapRoute("Default-Localized-UI-Culture", "{ui-culture:ui-culture}/{controller=Home}/{action=Index}/{id?}", null, notCertificateControllerConstraints);

				routeBuilder.MapRoute("Certificate-Localized", "Certificate/{culture:culture}/{ui-culture:ui-culture}/{action=Index}/{id?}", certificateControllerParameter, certificateControllerParameter);
				routeBuilder.MapRoute("Certificate-Localized-Culture", "Certificate/{culture:culture}/{action=Index}/{id?}", certificateControllerParameter, certificateControllerParameter);
				routeBuilder.MapRoute("Certificate-Localized-UI-Culture", "Certificate/{ui-culture:ui-culture}/{action=Index}/{id?}", certificateControllerParameter, certificateControllerParameter);
			});
		}

		public virtual void ConfigureServices(IServiceCollection services)
		{
			if(services == null)
				throw new ArgumentNullException(nameof(services));

			services.ConfigureRequestLocalization(this.Configuration);

			services.Configure<RouteOptions>(routeOptions =>
			{
				routeOptions.ConstraintMap.Add(RouteKeys.Culture, typeof(CultureRouteConstraint));
				routeOptions.ConstraintMap.Add(RouteKeys.UiCulture, typeof(UiCultureRouteConstraint));
			});

			services.AddMvc(mvcOptions =>
				{
					mvcOptions.Filters.Add(typeof(LocalizationFilter));
					mvcOptions.Filters.Add(new MiddlewareFilterAttribute(typeof(RequestLocalizationMiddlewareConfigurator)));
				}
			).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		#endregion
	}
}