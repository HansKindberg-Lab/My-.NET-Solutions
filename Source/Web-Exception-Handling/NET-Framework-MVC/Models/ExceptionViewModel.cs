using System;

namespace Company.WebApplication.Models
{
	public class ExceptionViewModel
	{
		#region Properties

		public virtual string Description { get; set; }
		public virtual string DetailedDescription { get; set; }
		public virtual Exception Exception { get; set; }
		public virtual string Heading { get; set; }
		public virtual int StatusCode { get; set; }
		public virtual int? SubStatusCode { get; set; }

		#endregion
	}
}