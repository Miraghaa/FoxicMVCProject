using System.ComponentModel.DataAnnotations;

namespace FoxicUI.ViewModels.AuthVM
{
	public class LoginVM
	{

		[Required, DataType(DataType.EmailAddress)]
		public string Email { get; set; } = null!;

		[Required, DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		public bool RememberMe { get; set; }
	}
}
