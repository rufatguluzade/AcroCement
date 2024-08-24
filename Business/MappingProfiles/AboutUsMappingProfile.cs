using AutoMapper;
using Business.DTOs.AboutUs.Request;
using Business.DTOs.AboutUs.Response;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles
{
    public class AboutUsMappingProfile : Profile
    {
        public AboutUsMappingProfile() :base()
        {
            CreateMap<AboutUS  , AboutUsCreateDTO>().ReverseMap();
            CreateMap<AboutUS, AboutUsUpdateDTO>().ReverseMap();
            CreateMap<AboutUsResponseDTO, AboutUS>().ReverseMap();
        }
    }
}
