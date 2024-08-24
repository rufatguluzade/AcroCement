using AutoMapper;
using Business.DTOs.Common;
using Business.DTOs.Slider.Response;
using Business.DTOs.WhyUs.Request;
using Business.DTOs.WhyUs.Response;
using Business.Exceptions;
using Business.Extensions;
using Business.Helpers;
using Business.Services.Abstraction;
using Business.Validators.Slider;
using Business.Validators.WhyUs;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Hosting;


namespace Business.Services.Concered
{
    public class WhyUsService : IWhyUsService
    {


        private readonly IWhyUsRepository _whyUsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IWebHostEnvironment _env;

        public WhyUsService(IWhyUsRepository whyUsRepository, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
           _whyUsRepository = whyUsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;


        }

        public async Task<Response> CreateAsync(WhyUsCreateDTO model)
        {
            var result = await new WhyUsCreateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var whyUs = _mapper.Map<WhyUs>(model);


            if (whyUs.ImageFile == null)
            {

                throw new ValidationException("Image daxil edilmelidir");
            }



            if (!whyUs.ImageFile.CheckFileSize(1000))
            {

                throw new ValidationException("Image olcusu max 1 mb olamlidir");

            }
            if (!whyUs.ImageFile.CheckFileType("image/jpeg"))
            {


                throw new ValidationException("Image jpg tipi olmalidir");
            }


            whyUs.ImageUrl = whyUs.ImageFile.CreateImage(_env, "img", "whyus");


            await _whyUsRepository.CreateAsync(whyUs);
        
            await _unitOfWork.CommitAsync();


            return new Response
            {
                Message = "data ugurla yaradildi"
            };
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var whyUs = await _whyUsRepository.GetAsync(id);

            if (whyUs == null)
            {

                throw new ValidationException("data tapilmadi");
            }

            _whyUsRepository.Delete(whyUs);
            await _unitOfWork.CommitAsync();



            return new Response
            {
                Message = "data ugurla silindi"
            };
        }

        public async Task<Response<List<WhyUsResponseDTO>>> GetAllAsync()
        {
            var response = await _whyUsRepository.GetAllAsync();

            if (response == null)
            {

                throw new NotFoundException("data tapilmadi");
            }



            return new Response<List<WhyUsResponseDTO>>
            {
                Data = _mapper.Map<List<WhyUsResponseDTO>>(response),
                Message = "data ugurla getirildi"
            };
        }

        public async Task<Response<WhyUsResponseDTO>> GetAsync(int id)
        {
            var whyUs = await _whyUsRepository.GetAsync(id);
            if (whyUs == null)
            {
                throw new NotFoundException("data tapilmadi");
            }


            return new Response<WhyUsResponseDTO>
            {
                Data = _mapper.Map<WhyUsResponseDTO>(whyUs),
                Message = "data ugurla getirildi"
            };
        }

        public async Task<Response> UpdateAsync(int id, WhyUsUpdateDTO model)
        {
            var result = await new WhyUsUpdateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }



            var existedWhyUs = await _whyUsRepository.GetAsync(id);

            if (existedWhyUs == null)
            {

                throw new NotFoundException("data tapilmadi");
            }
            _mapper.Map(model, existedWhyUs);


            if (existedWhyUs.ImageFile == null)
            {

                throw new ValidationException("Image daxil edilmelidir");
            }






            if (!existedWhyUs.ImageFile.CheckFileSize(1000))
            {
                throw new ValidationException("Image olcusu max 1 mb olamlidir");

            }
            if (!existedWhyUs.ImageFile.CheckFileType("image/jpeg"))
            {


                throw new ValidationException("Image jpg tipi olmalidir");

            }

            Helper.DeleteFile(_env, existedWhyUs.ImageUrl, "img", "whyus");
            existedWhyUs.ImageUrl = model.ImageFile.CreateImage(_env, "img", "whyus");



            existedWhyUs.ModifiedDate = DateTime.Now;


            _whyUsRepository.Update(existedWhyUs);

            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "data ugurla modified olundu"
            };
        }
    }
}
