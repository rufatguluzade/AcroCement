using AutoMapper;
using Business.DTOs.Slider.Request;
using Business.DTOs.Slider.Response;
using Business.DTOs.WhyUs.Request;
using Business.DTOs.WhyUs.Response;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles
{
    public class WhyUsMappingProfile :Profile
    {
        public WhyUsMappingProfile():base()
        {
            CreateMap<WhyUs, WhyUsCreateDTO>().ReverseMap();
            CreateMap<WhyUs, WhyUsUpdateDTO>().ReverseMap();
            CreateMap<WhyUsResponseDTO, WhyUs>().ReverseMap();
        }
    }
}
