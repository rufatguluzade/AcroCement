using AutoMapper;
using Business.DTOs.Blog.Request;
using Business.DTOs.Product.Request;
using Business.DTOs.Product.Response;
using Business.DTOs.Tag.Response;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles
{
    public class ProductMappingProfile :Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductCreateDTO>().ForMember(dest => dest.ProductImageFiles, opt => opt.MapFrom(src => src.ProductImageFiles)).ReverseMap();
            CreateMap<ProductCreateDTO, Product>().ReverseMap();
            CreateMap<Product, ProductResponseDTO>().ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category)).ReverseMap();

        }
    }
}
