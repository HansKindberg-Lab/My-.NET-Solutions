using Company.WebApplication.Models.ViewModels.Shared;

namespace Company.WebApplication.Models.ViewModels
{
	public interface IViewModel
	{
		#region Properties

		ILayout Layout { get; }

		#endregion
	}
}