using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Company.WebApplication
{
	public class Startup
	{
		#region Methods

		public virtual void Configure(IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseDeveloperExceptionPage();
			applicationBuilder.UseStaticFiles();
			applicationBuilder.UseMvcWithDefaultRoute();
		}

		public virtual void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
		}

		#endregion
	}
}