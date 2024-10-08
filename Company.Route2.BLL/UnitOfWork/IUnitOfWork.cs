using Company.Route2.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Route2.BLL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public  IDepartementRepository DepartementRepository { get; }
        public  IEmployeeRepository EmployeeRepository { get; }

        public Task<int> SaveChangesAsync();
    }

}
