using Company.Route2.BLL.Interfaces;
using Company.Route2.DAL.Data.Contexts;
using Company.Route2.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route2.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employees> , IEmployeeRepository
    {
        public  EmployeeRepository(AppDbContext _context):base(_context) 
        {

        }

        public async Task< IEnumerable<Employees>> GetByNameAsync(string Searchword)
        {
           return await _context.Employees.Where(e=>e.Name.ToUpper().Contains(Searchword.ToUpper())).Include(e=>e.WorkFor).ToListAsync();
        }


    }

}
