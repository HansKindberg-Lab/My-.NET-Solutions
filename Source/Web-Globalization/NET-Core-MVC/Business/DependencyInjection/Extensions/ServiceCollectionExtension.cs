using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Company.WebApplication.Business.Globalization;
using Company.WebApplication.Business.Globalization.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.WebApplication.Business.DependencyInjection.Extensions
{
	public static class ServiceCollectionExtension
	{
		#region Methods

		public static IServiceCollection ConfigureRequestLocalization(this IServiceCollection services, IConfiguration configuration)
		{
			if(services == null)
				throw new ArgumentNullException(nameof(services));

			if(configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			services.Configure<RequestLocalizationOptions>(options =>
			{
				var customCultures = configuration.GetSection("CustomCultures").Get<IDictionary<string, CustomCultureOptions>>();

				var requestLocalizationOptions = CreateRequestLocalizationOptions();

				var requestLocalizationSection = configuration.GetSection("RequestLocalization");

				requestLocalizationSection.Bind(requestLocalizationOptions);

				var defaultRequestCultureSection = requestLocalizationSection.GetSection("DefaultRequestCulture");
				var defaultRequestCulture = GetCulture(defaultRequestCultureSection.GetSection("Culture").Get<string>(), customCultures);
				var defaultRequestUiCulture = GetCulture(defaultRequestCultureSection.GetSection("UiCulture").Get<string>(), customCultures);

				options.DefaultRequestCulture = new RequestCulture(defaultRequestCulture, defaultRequestUiCulture);
				options.FallBackToParentCultures = requestLocalizationOptions.FallBackToParentCultures;
				options.FallBackToParentUICultures = requestLocalizationOptions.FallBackToParentUICultures;

				options.RequestCultureProviders.Clear();

				foreach(var item in requestLocalizationSection.GetSection("RequestCultureProviders").Get<IEnumerable<string>>())
				{
					var type = Type.GetType(item, true, true);

					options.RequestCultureProviders.Add((IRequestCultureProvider) Activator.CreateInstance(type));
				}

				options.SupportedCultures = requestLocalizationSection.GetSection("SupportedCultures").Get<IEnumerable<string>>().Select(item => GetCulture(item, customCultures)).ToList();
				options.SupportedUICultures = requestLocalizationSection.GetSection("SupportedUiCultures").Get<IEnumerable<string>>().Select(item => GetCulture(item, customCultures)).ToList();
			});

			return services;
		}

		private static RequestLocalizationOptions CreateRequestLocalizationOptions()
		{
			var requestLocalizationOptions = new RequestLocalizationOptions();

			requestLocalizationOptions.RequestCultureProviders.Clear();
			requestLocalizationOptions.SupportedCultures.Clear();
			requestLocalizationOptions.SupportedUICultures.Clear();

			return requestLocalizationOptions;
		}

		private static CultureInfo GetCulture(string name, IDictionary<string, CustomCultureOptions> customCultures)
		{
			if(customCultures == null)
				throw new ArgumentNullException(nameof(customCultures));

			// ReSharper disable ConvertIfStatementToReturnStatement
			if(customCultures.TryGetValue(name, out var options))
				return CultureInfo.ReadOnly(new CustomCulture(options.DisplayName, options.EnglishName, name, options.NativeName, options.ParentName));
			// ReSharper restore ConvertIfStatementToReturnStatement

			return CultureInfo.GetCultureInfo(name);
		}

		#endregion
	}
}