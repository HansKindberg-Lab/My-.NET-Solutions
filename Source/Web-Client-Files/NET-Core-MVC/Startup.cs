using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

		public virtual void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
		{
			applicationBuilder.UseBrowserLink();
			applicationBuilder.UseDeveloperExceptionPage();
			applicationBuilder.UseStaticFiles();
			applicationBuilder.UseMvc(routes => { routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}"); });
		}

		public virtual void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}

		#endregion
	}
}