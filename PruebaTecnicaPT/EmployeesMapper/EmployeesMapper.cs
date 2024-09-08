using AutoMapper;
using PruebaTecnicaPT.Models;
using PruebaTecnicaPT.Models.Dtos;

namespace PruebaTecnicaPT.EmployeesMapper
{
    public class EmployeesMapper : Profile
    {
        public EmployeesMapper()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, CreatedEmployeeDto>().ReverseMap();
        }
    }
}
