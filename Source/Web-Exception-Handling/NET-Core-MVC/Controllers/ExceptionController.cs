using System;
using System.Collections.Generic;
using System.Net;
using Company.WebApplication.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Company.WebApplication.Controllers
{
	public class ExceptionController : Controller
	{
		#region Fields

		private static readonly IDictionary<string, string> _descriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{"401", "Access is denied due to invalid credentials."},
			{"401.2", "Access is denied due to server configuration."},
			{"403", "Access is denied."},
			{"404", "File or directory not found."},
			{"405", "HTTP verb used to access this page is not allowed."},
			{"406", "Client browser does not accept the MIME type of the requested page."},
			{"412", "Precondition set by the client failed when evaluated on the Web server."},
			{"500", "There is a problem with the resource you are looking for, and it cannot be displayed."},
			{"501", "Header values specify a method that is not implemented."},
			{"502", "Web server received an invalid response while acting as a gateway or proxy server."}
		};

		private static readonly IDictionary<string, string> _detailedDescriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{"401", "You do not have permission to view this directory or page using the credentials that you supplied."},
			{"401.2", "You do not have permission to view this directory or page using the credentials that you supplied because your Web browser is sending a WWW-Authenticate header field that the Web server is not configured to accept."},
			{"403", "You do not have permission to view this directory or page using the credentials that you supplied."},
			{"404", "The resource you are looking for might have been removed, had its name changed, or is temporarily unavailable."},
			{"405", "The page you are looking for cannot be displayed because an invalid method (HTTP verb) was used to attempt access."},
			{"406", "The page you are looking for cannot be opened by your browser because it has a file name extension that your browser does not accept."},
			{"412", "The request was not completed due to preconditions that are set in the request header. Preconditions prevent the requested method from being applied to a resource other than the one intended. An example of a precondition is testing for expired content in the page cache of the client."},
			{"501", "The page you are looking for cannot be displayed because a header value in the request does not match certain configuration settings on the Web server. For example, a request header might specify a POST to a static file that cannot be posted to, or specify a Transfer-Encoding value that cannot make use of compression."},
			{"502", "There is a problem with the page you are looking for, and it cannot be displayed. When the Web server (while acting as a gateway or proxy) contacted the upstream content server, it received an invalid response from the content server."}
		};

		private static readonly IDictionary<int, string> _statusDescriptions = new Dictionary<int, string>
		{
			{401, "Unauthorized"},
			{403, "Forbidden"},
			{404, "Not found"},
			{405, "Incorrect HTTP verb"},
			{406, "Incorrect MIME type"},
			{412, "Incorrect precondition"},
			{500, "Internal server error"},
			{501, "Incorrect header"},
			{502, "Gateway error"}
		};

		#endregion

		#region Constructors

		public ExceptionController(IHostingEnvironment hostingEnvironment)
		{
			this.HostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
		}

		#endregion

		#region Properties

		protected internal virtual IDictionary<string, string> Descriptions => _descriptions;
		protected internal virtual IDictionary<string, string> DetailedDescriptions => _detailedDescriptions;
		protected internal virtual IHostingEnvironment HostingEnvironment { get; }
		protected internal virtual IDictionary<int, string> StatusDescriptions => _statusDescriptions;

		#endregion

		#region Methods

		protected internal virtual string GetDescription(int statusCode, int? subStatusCode)
		{
			return this.GetDictionaryValue(this.Descriptions, statusCode, subStatusCode);
		}

		protected internal virtual string GetDetailedDescription(int statusCode, int? subStatusCode)
		{
			return this.GetDictionaryValue(this.DetailedDescriptions, statusCode, subStatusCode);
		}

		protected internal virtual string GetDictionaryValue(IDictionary<string, string> dictionary, int statusCode, int? subStatusCode)
		{
			if(dictionary == null)
				throw new ArgumentNullException(nameof(dictionary));

			var statusLabel = this.GetStatusLabel(statusCode, subStatusCode);

			if(dictionary.TryGetValue(statusLabel, out var value))
				return value;

			// ReSharper disable InvertIf
			if(subStatusCode != null)
			{
				statusLabel = this.GetStatusLabel(statusCode, null);

				if(dictionary.TryGetValue(statusLabel, out var defaultValue))
					return defaultValue;
			}
			// ReSharper restore InvertIf

			return null;
		}

		protected internal virtual string GetStatusDescription(int statusCode)
		{
			return this.StatusDescriptions.TryGetValue(statusCode, out var value) ? value : ((HttpStatusCode) statusCode).ToString();
		}

		protected internal virtual string GetStatusLabel(int statusCode, int? subStatusCode)
		{
			return statusCode + (subStatusCode != null ? "." + subStatusCode : string.Empty);
		}

		public virtual IActionResult Index(int? statusCode, int? subStatusCode)
		{
			var model = new ExceptionViewModel
			{
				StatusCode = statusCode ?? 500,
				SubStatusCode = subStatusCode
			};

			model.Description = this.GetDescription(model.StatusCode, model.SubStatusCode);
			model.DetailedDescription = this.GetDetailedDescription(model.StatusCode, model.SubStatusCode);

			if(this.HostingEnvironment.IsDevelopment())
				model.Exception = this.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

			model.Heading = this.GetStatusLabel(model.StatusCode, model.SubStatusCode) + " " + this.GetStatusDescription(model.StatusCode);

			this.Response.StatusCode = model.StatusCode;

			return this.View(model);
		}

		public virtual IActionResult ThrowInvalidOperationException()
		{
			throw new InvalidOperationException();
		}

		#endregion
	}
}