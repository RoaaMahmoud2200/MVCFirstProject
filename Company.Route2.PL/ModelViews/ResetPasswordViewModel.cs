using System.ComponentModel.DataAnnotations;

namespace Company.Route2.PL.ModelViews
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required ")]
		[DataType(DataType.Password)]
		public string Password { get; set; }





		[Required(ErrorMessage = "ConfermedPassword is Required ")]
		[Compare(nameof(Password), ErrorMessage = "ConfirmedPassword should match Password")]
		[DataType(DataType.Password)]
		public string ConfirmedPassword { get; set; }
	}
}
