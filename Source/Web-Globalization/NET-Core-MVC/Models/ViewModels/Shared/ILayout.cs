using System;
using System.Collections.Generic;
using System.Globalization;
using Company.WebApplication.Business.Security.Cryptography;
using Company.WebApplication.Models.Navigation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace Company.WebApplication.Models.ViewModels.Shared
{
	public interface ILayout
	{
		#region Properties

		ICertificate Certificate { get; }
		CultureInfo Culture { get; }
		INavigationNode CultureNavigation { get; }
		INavigationNode MainNavigation { get; }
		DateTime Now { get; }
		IRequestCultureFeature RequestCultureFeature { get; }
		RequestLocalizationOptions RequestLocalizationOptions { get; }
		IDictionary<string, object> RouteDictionary { get; }
		CultureInfo UiCulture { get; }
		INavigationNode UiCultureNavigation { get; }

		#endregion
	}
}