using System.Security.Cryptography.X509Certificates;

namespace Company.WebApplication.Models
{
	public class ViewModel
	{
		#region Properties

		public virtual X509Certificate2 ClientCertificate { get; set; }

		#endregion
	}
}