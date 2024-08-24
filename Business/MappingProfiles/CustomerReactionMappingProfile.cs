using AutoMapper;
using Business.DTOs.Contact.Request;
using Business.DTOs.Contact.Response;
using Business.DTOs.CustomerReaction.Request;
using Business.DTOs.CustomerReaction.Response;
using Common.Entities;


namespace Business.MappingProfiles
{
    public class CustomerReactionMappingProfile :Profile
    {
        public CustomerReactionMappingProfile() :base()
        {
            CreateMap<CustomerReaction, CustomerReactionCreateDTO>().ReverseMap();
            CreateMap<CustomerReaction, CustomerReactionUpdateDTO>().ReverseMap();
            CreateMap<CustomerReactionResponseDTO, CustomerReaction>().ReverseMap();
        }
    }
}
