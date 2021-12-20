using AutoMapper;
using CaseItau.API.DTOs;
using CaseItau.API.Model;

namespace CaseItau.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Fundo, FundoDTO>().ReverseMap();
        }
    }
}