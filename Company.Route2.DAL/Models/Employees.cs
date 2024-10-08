using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route2.DAL.Models
{
    public class Employees : BaseClass
    {
        public  string Name { get; set; }
        public  string Email { get; set; }
        public  string Address { get; set; }
        public  string? ImageName { get; set; }
        public  int Age { get; set; }
        
        public  int PhoneNumber { get; set; }
        public  double Salary { get; set; }

        public  DateTime DateOfCreation { get; set; }=DateTime.Now; 
        public  DateTime DateOfHiring { get; set; }
        public  bool IsActive { get; set; }
        public  bool IsDeleted { get; set; }
        public int? WorkForId { get; set; }

        public Departement? WorkFor { get; set; }
    }
}
