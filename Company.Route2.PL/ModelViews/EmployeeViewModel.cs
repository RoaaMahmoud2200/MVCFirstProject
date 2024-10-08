using Company.Route2.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.Route2.PL.ModelViews
{
    public class EmployeeViewModel
    {
        public  int Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
           ErrorMessage = "address must be  like 123-Street-City-Country")]
        public string Address { get; set; }
        public int Age { get; set; }

        public int PhoneNumber { get; set; }
        [Required(ErrorMessage = "salary is required")]
        public double Salary { get; set; }

        public DateTime DateOfHiring { get; set; }
        public bool IsActive { get; set; }
        public int? WorkForId { get; set; }

        public Departement? WorkFor { get; set; }

        public  IFormFile? Image { get; set; }
        public  string? ImageName { get; set; }
    }
}
