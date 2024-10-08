using Company.Route2.BLL.Interfaces;
using Company.Route2.DAL.Data.Contexts;
using Company.Route2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route2.BLL.Repositories
{
    public class DepartementRepository :GenericRepository<Departement> , IDepartementRepository
    {

        public DepartementRepository(AppDbContext _Context):base(_Context)
        {

        }
        
    }
}
