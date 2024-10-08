using System.ComponentModel.DataAnnotations;

namespace Company.Route2.PL.ModelViews
{
	public class SignUpViewModel
	{
        [Required(ErrorMessage = "UserName is Required ")]
        public  string  UserName { get; set; }
		[Required(ErrorMessage = "Fname is Required ")]

		public string  Fname { get; set; }
		[Required(ErrorMessage = "Lname is Required ")]

		public string Lname { get; set; }
		[Required(ErrorMessage = "Email is Required ")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is Required ")]
		[DataType(DataType.Password)]	
		public string Password { get; set; }
		[Required(ErrorMessage = "ConfermedPassword is Required ")]
		[Compare(nameof(Password),ErrorMessage = "ConfirmedPassword should match Password")]
		[DataType(DataType.Password)]

		public string ConfirmedPassword { get; set; }
        public  bool IsAgree { get; set; }
		
    }
}
