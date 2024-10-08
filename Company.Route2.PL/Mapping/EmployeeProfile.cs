using AutoMapper;
using Company.Route2.DAL.Models;
using Company.Route2.PL.ModelViews;

namespace Company.Route2.PL.Mapping
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employees, EmployeeViewModel>().ReverseMap();
        }
    }
}
