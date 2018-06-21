using System;
using System.Web;
using System.Web.Mvc;
using Company.WebApplication.Models;

namespace Company.WebApplication.Controllers
{
	public class ExceptionController : SiteController
	{
		#region Constructors

		public ExceptionController() { }

		public ExceptionController(Exception exception)
		{
			this.Exception = exception;
		}

		#endregion

		#region Properties

		protected internal virtual Exception Exception { get; }

		#endregion

		#region Methods

		public virtual ActionResult Catch(int statusCode, int? subStatusCode)
		{
			if(statusCode == 403 && subStatusCode != null && subStatusCode.Value == 16)
				return this.CreateDefaultView(this.Request.ClientCertificate);

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

		protected internal virtual int GetStatusCode(Exception exception)
		{
			if(exception is HttpException httpException)
				return httpException.GetHttpCode();

			return 500;
		}

		protected internal virtual string GetStatusDescription(int statusCode)
		{
			return HttpWorkerRequest.GetStatusDescription(statusCode);
		}

		protected internal virtual int? GetSubStatusCode(Exception exception)
		{
			if(exception is HttpException httpException && httpException.WebEventCode > 0)
				return httpException.WebEventCode;

			return null;
		}

		public virtual ActionResult Index(int? statusCode, int? subStatusCode)
		{
			var model = new ExceptionViewModel
			{
				StatusCode = statusCode ?? this.GetStatusCode(this.Exception),
				SubStatusCode = subStatusCode ?? this.GetSubStatusCode(this.Exception)
			};

			model.Description = this.GetDescription(model.StatusCode);
			model.DetailedDescription = this.GetDetailedDescription(model.StatusCode);

			if(this.HttpContext.IsDebuggingEnabled)
				model.Exception = this.Exception;

			model.Heading = model.StatusCode + (model.SubStatusCode != null ? "." + model.SubStatusCode : string.Empty) + " " + this.GetStatusDescription(model.StatusCode);

			this.Response.StatusCode = model.StatusCode;

			if(model.SubStatusCode != null)
				this.Response.SubStatusCode = model.SubStatusCode.Value;

			this.Server.ClearError();

			return this.View(model);
		}

		public virtual ActionResult ThrowInvalidOperationException()
		{
			throw new InvalidOperationException();
		}

		#endregion
	}
}