using Company.Route2.BLL.Interfaces;
using Company.Route2.BLL.Repositories;
using Company.Route2.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route2.BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private IDepartementRepository _departementRepository;
        private IEmployeeRepository _employeeRepository;

        public UnitOfWork(AppDbContext _context)
        {
            _departementRepository = new DepartementRepository(_context);
            _employeeRepository = new EmployeeRepository(_context);
            context = _context;
        }
        public IDepartementRepository DepartementRepository { get => _departementRepository; }
        public IEmployeeRepository EmployeeRepository { get => _employeeRepository; }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();   
        }
    }
}
