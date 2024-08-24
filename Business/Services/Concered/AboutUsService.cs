
using AutoMapper;
using Business.DTOs.AboutUs.Request;
using Business.DTOs.AboutUs.Response;
using Business.DTOs.Common;
using Business.Exceptions;
using Business.Extensions;
using Business.Helpers;
using Business.Services.Abstraction;
using Business.Validators.AboutUs;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Hosting;

namespace Business.Services.Concered
{

    public class AboutUsService : IAboutUsService
    {


        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
     
        private readonly IWebHostEnvironment _env;

        public AboutUsService(IAboutUsRepository aboutUsRepository, IUnitOfWork unitOfWork , IMapper mapper , IWebHostEnvironment env)
        {
            _aboutUsRepository = aboutUsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
          
                
        }
        public async Task<Response> CreateAsync(AboutUsCreateDTO model)
        {
            var result = await new AboutUsCreateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var aboutUs = _mapper.Map<AboutUS>(model);


            if (aboutUs.ImageFile == null)
            {
              
                throw new ValidationException("Image daxil edilmelidir");
            }



            if (!aboutUs.ImageFile.CheckFileSize(1000))
            {
               
                throw new ValidationException("Image olcusu max 1 mb olamlidir");

            }
            if (!aboutUs.ImageFile.CheckFileType("image/jpeg"))
            {
             

                throw new ValidationException("Image jpg tipi olmalidir");

            }


            aboutUs.ImageUrl = aboutUs.ImageFile.CreateImage(_env, "img", "about");



            await _aboutUsRepository.CreateAsync(aboutUs);
            await _unitOfWork.CommitAsync();

         
            return new Response
            {
                Message = "AboutUS ugurla yaradildi"
            };

        }

        public async Task<Response> DeleteAsync(int id)
        {
            var aboutUs = await _aboutUsRepository.GetAsync(id);

            if (aboutUs == null)
            {
               
                throw new ValidationException("aboutUs tapilmadi");
            }

            _aboutUsRepository.Delete(aboutUs);
            await _unitOfWork.CommitAsync();


           
            return new Response
            {
                Message = "aboutUs ugurla silindi"
            };
        }
    

        public  async Task<Response<List<AboutUsResponseDTO>>> GetAllAsync()
        {
            var response = await _aboutUsRepository.GetAllAsync();

            if (response == null)
            {
               
                throw new NotFoundException("aboutUs tapilmadi");
            }


          
            return new Response<List<AboutUsResponseDTO>>
            {
                Data = _mapper.Map<List<AboutUsResponseDTO>>(response),
                Message = "aboutUs ugurla getirildi"
            };


        }

        public async Task<Response<AboutUsResponseDTO>> GetAsync(int id)
        {
            var aboutUs = await _aboutUsRepository.GetAsync(id);
            if (aboutUs == null)
            {
                throw new NotFoundException("aboutUs tapilmadi");
            }


            return new Response<AboutUsResponseDTO>
            {
                Data = _mapper.Map<AboutUsResponseDTO>(aboutUs),
                Message = "data ugurla getirildi"
            };
        }

        public async Task<Response> UpdateAsync(int id, AboutUsUpdateDTO model)
        {
            var result = await new AboutUsUpdateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

         

            var existedaboutUs = await _aboutUsRepository.GetAsync(id);

            if (existedaboutUs == null)
            {
               
                throw new NotFoundException("aboutUs tapilmadi");
            }
            _mapper.Map(model, existedaboutUs);


            if (existedaboutUs.ImageFile == null)
            {
           
                throw new ValidationException("Image daxil edilmelidir");
            }






            if (!existedaboutUs.ImageFile.CheckFileSize(1000))
            {
                throw new ValidationException("Image olcusu max 1 mb olamlidir");

            }
            if (!existedaboutUs.ImageFile.CheckFileType("image/jpeg"))
            {
 

                throw new ValidationException("Image jpg tipi olmalidir");

            }

            Helper.DeleteFile(_env, existedaboutUs.ImageUrl, "img", "about");
            existedaboutUs.ImageUrl = model.ImageFile.CreateImage(_env, "img", "about");



            existedaboutUs.ModifiedDate = DateTime.Now;


            _aboutUsRepository.Update(existedaboutUs);

            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "aboutUs ugurla modified olundu"
            };
        }
    }
}
