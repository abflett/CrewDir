using AutoMapper;
using CrewDir.Api.DTOs.Requests;
using CrewDir.Api.DTOs.Responses;
using CrewDir.Api.Models;

namespace CrewDir.Api.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DepartmentRequest, Department>();
            CreateMap<Department, DepartmentResponse>();

            CreateMap<Employee, EmployeeResponse>();
            CreateMap<EmployeeRequest, Employee>();
        }
    }
}
