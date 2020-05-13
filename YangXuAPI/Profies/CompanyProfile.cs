using AutoMapper;
using YangXuAPI.Entities;
using YangXuAPI.Models;

namespace YangXuAPI.Profies
{
    public class CompanyProfile:Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.CompanyName
                    , opt => opt
                        .MapFrom(src => src.Name));
            CreateMap<CompanyAddDto, Company>();
        }
    }
}
