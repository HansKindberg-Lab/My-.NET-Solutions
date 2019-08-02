using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace Company.WebApplication.Business.Web.Mvc.Configuration
{
	public class RequestLocalizationMiddlewareConfigurator
	{
		#region Methods

		public virtual void Configure(IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseMiddleware<RequestLocalizationMiddleware>();
		}

		#endregion
	}
}