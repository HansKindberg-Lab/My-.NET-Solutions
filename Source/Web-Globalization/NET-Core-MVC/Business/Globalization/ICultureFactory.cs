using System.Globalization;

namespace Company.WebApplication.Business.Globalization
{
	public interface ICultureFactory
	{
		#region Methods

		CultureInfo Create(string name);
		CultureInfo Create(string name, bool readOnly);

		#endregion
	}
}