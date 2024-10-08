using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route2.DAL.Models
{
    public class Departement : BaseClass
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public  ICollection<Employees>? Employees { get; set; }
    }
}
