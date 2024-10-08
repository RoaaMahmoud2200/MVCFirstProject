using Company.Route2.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.Route2.PL.ModelViews
{
    public class DepartmentViewModel
    {
        public  int  Id { get; set; }
        [Required(ErrorMessage = "the Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "the code is required")]
        public string Code { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public ICollection<Employees>? Employees { get; set; }
    }
}
