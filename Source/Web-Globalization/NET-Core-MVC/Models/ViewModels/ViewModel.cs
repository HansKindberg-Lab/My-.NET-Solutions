using System;
using Company.WebApplication.Models.ViewModels.Shared;
using Microsoft.AspNetCore.Http;

namespace Company.WebApplication.Models.ViewModels
{
	public class ViewModel : IViewModel
	{
		#region Fields

		private ILayout _layout;

		#endregion

		#region Constructors

		public ViewModel(HttpContext httpContext)
		{
			this.HttpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
		}

		#endregion

		#region Properties

		protected internal virtual HttpContext HttpContext { get; }
		public virtual ILayout Layout => this._layout ?? (this._layout = new Layout(this.HttpContext));

		#endregion
	}
}