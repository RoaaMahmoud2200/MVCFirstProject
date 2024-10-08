using System.ComponentModel.DataAnnotations;

namespace Company.Route2.PL.ModelViews
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required ")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
