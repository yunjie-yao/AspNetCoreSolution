using System;
using AutoMapper;
using YangXuAPI.Entities;
using YangXuAPI.Models;

namespace YangXuAPI.Profies
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Name,
                    opt => opt
                        .MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.GenderDisplay,
                    opt => opt
                        .MapFrom(src => src.Gender.ToString()))
                .ForMember(dest => dest.Age,
                    opt => opt
                        .MapFrom(src => DateTime.Now.Year - src.DateOfBirth.Year));
            CreateMap<EmployeeAddDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();

        }
    }
}
