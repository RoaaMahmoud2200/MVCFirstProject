using AutoMapper;
using Company.Route2.DAL.Models;
using Company.Route2.PL.ModelViews;

namespace Company.Route2.PL.Mapping
{
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Departement,DepartmentViewModel>().ReverseMap();
        }

    }
}
