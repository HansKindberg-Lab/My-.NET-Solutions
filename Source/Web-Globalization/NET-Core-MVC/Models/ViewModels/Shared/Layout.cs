using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Company.WebApplication.Business.Security.Cryptography;
using Company.WebApplication.Business.Web.Routing;
using Company.WebApplication.Models.Navigation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Company.WebApplication.Models.ViewModels.Shared
{
	// ReSharper disable InvertIf
	public class Layout : ILayout
	{
		#region Fields

		private Lazy<ICertificate> _certificate;
		private const string _certificateController = "Certificate";
		private Lazy<string> _controllerSegment;
		private Lazy<string> _cultureCookieValue;
		private INavigationNode _cultureNavigation;
		private Lazy<string> _cultureSegment;
		private const string _homeController = "Home";
		private INavigationNode _mainNavigation;
		private static readonly IEnumerable<string> _navigationControllers = new[] {"First", "Second", "Third", _certificateController};
		private Lazy<DateTime> _now;
		private IEnumerable<string> _pathSegments;
		private IEnumerable<string> _pathSegmentsWithoutController;
		private IEnumerable<string> _pathSegmentsWithoutControllerAndCultureAndUiCulture;
		private const char _pathSeparator = '/';
		private Lazy<IRequestCultureFeature> _requestCultureFeature;
		private Lazy<RequestLocalizationOptions> _requestLocalizationOptions;
		private IDictionary<string, object> _routeDictionary;
		private INavigationNode _uiCultureNavigation;
		private Lazy<string> _uiCultureSegment;

		#endregion

		#region Constructors

		public Layout(HttpContext httpContext)
		{
			this.HttpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
		}

		#endregion

		#region Properties

		public virtual ICertificate Certificate
		{
			get
			{
				if(this._certificate == null)
					this._certificate = new Lazy<ICertificate>(() => (X509Certificate2Wrapper) this.HttpContext.Connection.ClientCertificate);

				return this._certificate.Value;
			}
		}

		protected internal virtual string ControllerSegment
		{
			get
			{
				if(this._controllerSegment == null)
				{
					this._controllerSegment = new Lazy<string>(() =>
					{
						if(this.HttpContext.GetRouteValue(RouteKeys.Controller) is string controller)
							return this.PathSegments.FirstOrDefault(segment => controller.Equals(segment, StringComparison.OrdinalIgnoreCase));

						return null;
					});
				}

				return this._controllerSegment.Value;
			}
		}

		public virtual CultureInfo Culture => CultureInfo.CurrentCulture;
		public virtual string CultureCookieName => CookieRequestCultureProvider.DefaultCookieName;

		public virtual string CultureCookieValue
		{
			get
			{
				if(this._cultureCookieValue == null)
					this._cultureCookieValue = new Lazy<string>(() => this.HttpContext.Request.Cookies.TryGetValue(this.CultureCookieName, out var value) ? value : null);

				return this._cultureCookieValue.Value;
			}
		}

		public virtual INavigationNode CultureNavigation
		{
			get { return this._cultureNavigation ?? (this._cultureNavigation = this.GetCultureNavigation(() => this.Culture, () => this.CultureSegment, "Culture", "ui-culture", () => this.RequestLocalizationOptions.SupportedCultures, this.GetCultureUrl)); }
		}

		protected internal virtual string CultureSegment
		{
			get
			{
				if(this._cultureSegment == null)
				{
					this._cultureSegment = new Lazy<string>(() =>
					{
						if(this.HttpContext.GetRouteValue(RouteKeys.Culture) is string culture)
						{
							var potentialCultureSegment = this.PathSegmentsWithoutController.FirstOrDefault();

							if(potentialCultureSegment != null && string.Equals(culture, potentialCultureSegment, StringComparison.OrdinalIgnoreCase))
								return potentialCultureSegment;
						}

						return null;
					});
				}

				return this._cultureSegment.Value;
			}
		}

		protected internal virtual HttpContext HttpContext { get; }

		public virtual INavigationNode MainNavigation
		{
			get
			{
				if(this._mainNavigation == null)
				{
					var mainNavigation = new NavigationNode(null)
					{
						Active = this.GetNavigationActive(_homeController),
						Text = _homeController,
						Tooltip = this.GetNavigationTooltip("start"),
						Url = this.GetNavigationUrl(_homeController)
					};

					foreach(var item in _navigationControllers)
					{
						mainNavigation.Children.Add(new NavigationNode(mainNavigation)
						{
							Active = this.GetNavigationActive(item),
							Text = item,
							Tooltip = this.GetNavigationTooltip(item),
							Url = this.GetNavigationUrl(item)
						});
					}

					this._mainNavigation = mainNavigation;
				}

				return this._mainNavigation;
			}
		}

		public virtual DateTime Now
		{
			get
			{
				if(this._now == null)
					this._now = new Lazy<DateTime>(() => DateTime.Now);

				return this._now.Value;
			}
		}

		protected internal virtual IEnumerable<string> PathSegments => this._pathSegments ?? (this._pathSegments = this.HttpContext.Request.Path.Value.Split(_pathSeparator, StringSplitOptions.RemoveEmptyEntries));

		protected internal virtual IEnumerable<string> PathSegmentsWithoutController
		{
			get
			{
				if(this._pathSegmentsWithoutController == null)
				{
					var pathSegments = this.PathSegments.ToList();

					if(this.HttpContext.GetRouteValue(RouteKeys.Controller) is string controller)
					{
						for(var i = 0; i < pathSegments.Count; i++)
						{
							if(string.Equals(controller, pathSegments[i], StringComparison.OrdinalIgnoreCase))
							{
								pathSegments.RemoveAt(i);
								break;
							}
						}
					}

					this._pathSegmentsWithoutController = pathSegments.ToArray();
				}

				return this._pathSegmentsWithoutController;
			}
		}

		protected internal virtual IEnumerable<string> PathSegmentsWithoutControllerAndCultureAndUiCulture
		{
			get
			{
				if(this._pathSegmentsWithoutControllerAndCultureAndUiCulture == null)
				{
					var pathSegments = this.PathSegmentsWithoutController.ToList();

					if(this.CultureSegment != null && pathSegments.Any())
						pathSegments.RemoveAt(0);

					if(this.UiCultureSegment != null && pathSegments.Any())
						pathSegments.RemoveAt(0);

					this._pathSegmentsWithoutControllerAndCultureAndUiCulture = pathSegments.ToArray();
				}

				return this._pathSegmentsWithoutControllerAndCultureAndUiCulture;
			}
		}

		public virtual IRequestCultureFeature RequestCultureFeature
		{
			get
			{
				if(this._requestCultureFeature == null)
					this._requestCultureFeature = new Lazy<IRequestCultureFeature>(() => this.HttpContext.Features.Get<IRequestCultureFeature>());

				return this._requestCultureFeature.Value;
			}
		}

		public virtual RequestLocalizationOptions RequestLocalizationOptions
		{
			get
			{
				if(this._requestLocalizationOptions == null)
					this._requestLocalizationOptions = new Lazy<RequestLocalizationOptions>(() => this.HttpContext.RequestServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

				return this._requestLocalizationOptions.Value;
			}
		}

		public virtual IDictionary<string, object> RouteDictionary
		{
			get
			{
				if(this._routeDictionary == null)
				{
					var routeValueDictionary = this.HttpContext.GetRouteData().Values;

					this._routeDictionary = routeValueDictionary.Keys.OrderBy(key => key).ToDictionary(key => key, key => routeValueDictionary[key]);
				}

				return this._routeDictionary;
			}
		}

		public virtual CultureInfo UiCulture => CultureInfo.CurrentUICulture;

		public virtual INavigationNode UiCultureNavigation
		{
			get { return this._uiCultureNavigation ?? (this._uiCultureNavigation = this.GetCultureNavigation(() => this.UiCulture, () => this.UiCultureSegment, "UI-culture", "culture", () => this.RequestLocalizationOptions.SupportedUICultures, this.GetUiCultureUrl)); }
		}

		protected internal virtual string UiCultureSegment
		{
			get
			{
				if(this._uiCultureSegment == null)
				{
					this._uiCultureSegment = new Lazy<string>(() =>
					{
						if(this.HttpContext.GetRouteValue(RouteKeys.UiCulture) is string uiCulture)
						{
							var potentialUiCultureSegment = this.CultureSegment != null ? this.PathSegmentsWithoutController.Count() > 1 ? this.PathSegmentsWithoutController.ElementAt(1) : null : this.PathSegmentsWithoutController.FirstOrDefault();

							if(potentialUiCultureSegment != null && string.Equals(uiCulture, potentialUiCultureSegment, StringComparison.OrdinalIgnoreCase))
								return potentialUiCultureSegment;
						}

						return null;
					});
				}

				return this._uiCultureSegment.Value;
			}
		}

		#endregion

		#region Methods

		protected internal virtual INavigationNode GetCultureNavigation(Func<CultureInfo> cultureFunction, Func<string> cultureSegmentFunction, string label, string otherRouteKey, Func<IEnumerable<CultureInfo>> supportedCulturesFunction, Func<CultureInfo, Uri> urlFunction)
		{
			if(cultureFunction == null)
				throw new ArgumentNullException(nameof(cultureFunction));

			if(cultureSegmentFunction == null)
				throw new ArgumentNullException(nameof(cultureSegmentFunction));

			if(supportedCulturesFunction == null)
				throw new ArgumentNullException(nameof(supportedCulturesFunction));

			if(urlFunction == null)
				throw new ArgumentNullException(nameof(urlFunction));

			var culture = cultureFunction();
			var cultureSegment = cultureSegmentFunction();

			var cultureNavigation = new NavigationNode(null)
			{
				Active = culture != null,
				Text = this.GetCultureNavigationText(culture, label),
				Tooltip = this.GetCultureNavigationTooltip(cultureSegment, label, otherRouteKey)
			};

			if(cultureSegment != null)
			{
				cultureNavigation.Children.Add(new NavigationNode(cultureNavigation)
				{
					Text = " - Clear - ",
					Tooltip = "Clear the url-selected culture.",
					Url = urlFunction(null)
				});
			}

			foreach(var supportedCulture in supportedCulturesFunction().OrderBy(item => item.NativeName, StringComparer.Ordinal))
			{
				cultureNavigation.Children.Add(new NavigationNode(cultureNavigation)
				{
					Active = cultureSegment != null && supportedCulture.Equals(culture),
					Text = supportedCulture.NativeName,
					Tooltip = "Select culture " + supportedCulture.Name + ".",
					Url = urlFunction(supportedCulture)
				});
			}

			return cultureNavigation;
		}

		protected internal virtual string GetCultureNavigationText(CultureInfo culture, string label)
		{
			var hint = this.GetRequestCultureProviderHint(label, this.RequestCultureFeature.Provider);
			return label + (hint != null ? $" from {hint}" : null) + ": " + culture?.NativeName;
		}

		protected internal virtual string GetCultureNavigationTooltip(string cultureSegment, string label, string otherRouteKey)
		{
			string information = null;
			string informationArgument = null;
			const string informationFormat = "The current {0} is determined by {1}.";
			var labelAsLowerCase = label?.ToLowerInvariant();
			const string tooltipFormat = "Select a {0}.{1}";

			switch(this.RequestCultureFeature.Provider)
			{
				case null:
					informationArgument = "the default settings";
					break;
				case AcceptLanguageHeaderRequestCultureProvider _:
					informationArgument = "the request-header (browser-settings)";
					break;
				case CookieRequestCultureProvider _:
					informationArgument = "a cookie";
					break;
				case QueryStringRequestCultureProvider _:
					informationArgument = this.HttpContext.Request.Query.Keys.Contains(label, StringComparer.OrdinalIgnoreCase) ? "the query-string" : "the query-string or the default settings";
					break;
				case RouteDataRequestCultureProvider _:
					informationArgument = (cultureSegment == null ? "the " + otherRouteKey + "-route-key of " : null) + "the url";
					break;
				default:
					break;
			}

			if(informationArgument != null)
				information = " " + string.Format(this.Culture, informationFormat, labelAsLowerCase, informationArgument);

			return string.Format(this.Culture, tooltipFormat, labelAsLowerCase, information);
		}

		protected internal virtual Uri GetCultureUrl(CultureInfo culture)
		{
			var segments = new List<string>();

			if(culture != null)
				segments.Add(culture.Name);

			if(this.UiCultureSegment != null)
				segments.Add(this.UiCultureSegment);

			return this.GetCultureUrl(segments);
		}

		protected internal virtual Uri GetCultureUrl(IEnumerable<string> cultureSegments)
		{
			var segments = new List<string>(cultureSegments ?? Enumerable.Empty<string>());

			if(this.ControllerSegment != null)
			{
				if(string.Equals(this.ControllerSegment, _certificateController, StringComparison.OrdinalIgnoreCase))
					segments.Insert(0, this.ControllerSegment);
				else if(!string.Equals(this.ControllerSegment, _homeController, StringComparison.OrdinalIgnoreCase))
					segments.Add(this.ControllerSegment);
			}

			segments.AddRange(this.PathSegmentsWithoutControllerAndCultureAndUiCulture);

			return new Uri(_pathSeparator + (segments.Any() ? string.Join(_pathSeparator, segments) + _pathSeparator : string.Empty), UriKind.Relative);
		}

		protected internal virtual bool GetNavigationActive(string controller)
		{
			if(string.IsNullOrWhiteSpace(controller))
				return false;

			if(controller.Equals(_homeController, StringComparison.OrdinalIgnoreCase))
				return !this.PathSegments.Intersect(_navigationControllers, StringComparer.OrdinalIgnoreCase).Any();

			return this.PathSegments.Contains(controller, StringComparer.OrdinalIgnoreCase);
		}

		protected internal virtual string GetNavigationTooltip(string pageName)
		{
			return "Go to the " + (pageName ?? string.Empty).ToLowerInvariant() + "-page.";
		}

		protected internal virtual Uri GetNavigationUrl(string controller)
		{
			var segments = new List<string>();

			if(this.CultureSegment != null)
				segments.Add(this.CultureSegment);

			if(this.UiCultureSegment != null)
				segments.Add(this.UiCultureSegment);

			if(string.Equals(controller, _certificateController, StringComparison.OrdinalIgnoreCase))
				segments.Insert(0, controller);
			else if(!string.Equals(controller, _homeController, StringComparison.OrdinalIgnoreCase))
				segments.Add(controller);

			return new Uri(_pathSeparator + (segments.Any() ? string.Join(_pathSeparator, segments) + _pathSeparator : string.Empty), UriKind.Relative);
		}

		protected internal virtual string GetRequestCultureProviderHint(string label, IRequestCultureProvider requestCultureProvider)
		{
			switch(requestCultureProvider)
			{
				case null:
					return "default-settings";
				case AcceptLanguageHeaderRequestCultureProvider _:
					return "header";
				case CookieRequestCultureProvider _:
					return "cookie";
				case QueryStringRequestCultureProvider _:
					return this.HttpContext.Request.Query.Keys.Contains(label, StringComparer.OrdinalIgnoreCase) ? "query-string" : "query-string/default-settings";
				case RouteDataRequestCultureProvider _:
					return "url";
				default:
					return null;
			}
		}

		protected internal virtual Uri GetUiCultureUrl(CultureInfo uiCulture)
		{
			var segments = new List<string>();

			if(this.CultureSegment != null)
				segments.Add(this.CultureSegment);

			if(uiCulture != null)
				segments.Add(uiCulture.Name);

			return this.GetCultureUrl(segments);
		}

		#endregion
	}
	// ReSharper restore InvertIf
}