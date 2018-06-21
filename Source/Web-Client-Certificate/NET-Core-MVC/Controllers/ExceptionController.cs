using System;
using System.Net;
using Company.WebApplication.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Company.WebApplication.Controllers
{
	public class ExceptionController : SiteController
	{
		#region Constructors

		public ExceptionController(IHostingEnvironment hostingEnvironment)
		{
			this.HostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
		}

		#endregion

		#region Properties

		protected internal virtual IHostingEnvironment HostingEnvironment { get; }

		#endregion

		#region Methods

		public virtual IActionResult Catch(int statusCode, int? subStatusCode)
		{
			if(statusCode == 403 && subStatusCode != null && subStatusCode.Value == 16)
				return this.CreateDefaultView(this.HttpContext.Connection.ClientCertificate);

			return this.Index(statusCode, subStatusCode);
		}

		protected internal virtual string GetDescription(int statusCode)
		{
			return null;
		}

		protected internal virtual string GetDetailedDescription(int statusCode)
		{
			return null;
		}

		protected internal virtual string GetStatusDescription(int statusCode)
		{
			return ((HttpStatusCode) statusCode).ToString();
		}

		public virtual IActionResult Index(int? statusCode, int? subStatusCode)
		{
			var model = new ExceptionViewModel
			{
				StatusCode = statusCode ?? 500,
				SubStatusCode = subStatusCode
			};

			model.Description = this.GetDescription(model.StatusCode);
			model.DetailedDescription = this.GetDetailedDescription(model.StatusCode);

			if(this.HostingEnvironment.IsDevelopment())
				model.Exception = this.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

			model.Heading = model.StatusCode + (model.SubStatusCode != null ? "." + model.SubStatusCode : string.Empty) + " " + this.GetStatusDescription(model.StatusCode);

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