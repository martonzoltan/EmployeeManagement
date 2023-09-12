using AutoMapper;
using Shared.Models.DTO;
using Shared.Models.Entities;
using Shared.Models.ViewModels;

namespace Shared.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EmployeeDto, EmployeeViewModel>();
        CreateMap<EmployeeViewModel, EmployeeDto>();
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();
    }
}