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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseClass
    {
        private protected AppDbContext _context { get; }

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task CreateAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);  
           
        }

        public void Delete(T entity)
        {

            _context.Set<T>().Remove(entity);
           
        }

        public async Task<T> GetAsync(int? id)
        {
            
            return await _context.Set<T>().FindAsync(id);
        }

        public  async Task< IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employees))
                return (IEnumerable<T>)  await  _context.Employees.Include(e => e.WorkFor).ToListAsync();
            return await _context.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
             
                }
    }

}
