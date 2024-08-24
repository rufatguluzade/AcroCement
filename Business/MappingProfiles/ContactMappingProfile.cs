using AutoMapper;
using Business.DTOs.AboutUs.Request;
using Business.DTOs.AboutUs.Response;
using Business.DTOs.Contact.Request;
using Business.DTOs.Contact.Response;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile() :base() 
        {
            CreateMap<Contact, ContactCreateDTO>().ReverseMap();
            CreateMap<Contact, ContactUpdateDTO>().ReverseMap();
            CreateMap<ContactResponseDTO, Contact>().ReverseMap();
        }
    }
}
