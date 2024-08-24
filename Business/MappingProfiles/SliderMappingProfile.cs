using AutoMapper;
using Business.DTOs.Slider.Request;
using Business.DTOs.Slider.Response;
using Common.Entities;


namespace Business.MappingProfiles
{
    public class SliderMappingProfile :Profile
    {
        public SliderMappingProfile() :base()
        {

            CreateMap<Slider, SliderCreateDTO>().ReverseMap();
            CreateMap<Slider, SliderUpdateDTO>().ReverseMap();
            CreateMap<SliderResponseDTO, Slider>().ReverseMap();
        }
    }
}
