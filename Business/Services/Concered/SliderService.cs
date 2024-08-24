using AutoMapper;
using Business.DTOs.Common;

using Business.DTOs.Slider.Request;
using Business.DTOs.Slider.Response;
using Business.Exceptions;
using Business.Extensions;
using Business.Helpers;
using Business.Services.Abstraction;

using Business.Validators.Slider;
using Common.Entities;
using DataAccess.Repositories.Abstract;

using DataAccess.UnitOfWork;

using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concered
{
    public class SliderService : ISliderService
    {

        private readonly ISliderRepository _sliderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IWebHostEnvironment _env;

        public SliderService(ISliderRepository sliderRepository, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
           _sliderRepository = sliderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;


        }

        public async Task<Response> CreateAsync(SliderCreateDTO model)
        {
            var result = await new SliderCreateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var slider = _mapper.Map<Slider>(model);


            if (slider.ImageFile == null)
            {

                throw new ValidationException("Image daxil edilmelidir");
            }



            if (!slider.ImageFile.CheckFileSize(1000))
            {

                throw new ValidationException("Image olcusu max 1 mb olamlidir");

            }
            if (!slider.ImageFile.CheckFileType("image/jpeg"))
            {


                throw new ValidationException("Image jpg tipi olmalidir");
            }


            slider.ImageUrl = slider.ImageFile.CreateImage(_env, "img", "slider");



            await _sliderRepository.CreateAsync(slider);
            await _unitOfWork.CommitAsync();


            return new Response
            {
                Message = "Slider ugurla yaradildi"
            };
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var slider = await _sliderRepository.GetAsync(id);

            if (slider == null)
            {

                throw new ValidationException("Slider tapilmadi");
            }

            _sliderRepository.Delete(slider);
            await _unitOfWork.CommitAsync();



            return new Response
            {
                Message = "Slider ugurla silindi"
            };
        }

        public async Task<Response<List<SliderResponseDTO>>> GetAllAsync()
        {
            var response = await _sliderRepository.GetAllAsync();

            if (response == null)
            {

                throw new NotFoundException("Slide tapilmadi");
            }



            return new Response<List<SliderResponseDTO>>
            {
                Data = _mapper.Map<List<SliderResponseDTO>>(response),
                Message = "Slider ugurla getirildi"
            };
        }

        public async Task<Response<SliderResponseDTO>> GetAsync(int id)
        {
            var sliders = await _sliderRepository.GetAsync(id);
            if (sliders == null)
            {
                throw new NotFoundException("Slider tapilmadi");
            }


            return new Response<SliderResponseDTO>
            {
                Data = _mapper.Map<SliderResponseDTO>(sliders),
                Message = "data ugurla getirildi"
            };
        }

        public async Task<Response> UpdateAsync(int id, SliderUpdateDTO model)
        {
            var result = await new SliderUpdateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }



            var existedSlider = await _sliderRepository.GetAsync(id);

            if (existedSlider == null)
            {

                throw new NotFoundException("Slider tapilmadi");
            }
            _mapper.Map(model, existedSlider);


            if (existedSlider.ImageFile == null)
            {

                throw new ValidationException("Image daxil edilmelidir");
            }






            if (!existedSlider.ImageFile.CheckFileSize(1000))
            {
                throw new ValidationException("Image olcusu max 1 mb olamlidir");

            }
            if (!existedSlider.ImageFile.CheckFileType("image/jpeg"))
            {


                throw new ValidationException("Image jpg tipi olmalidir");

            }

            Helper.DeleteFile(_env, existedSlider.ImageUrl, "img", "slider");
            existedSlider.ImageUrl = model.ImageFile.CreateImage(_env, "img", "slider");



            existedSlider.ModifiedDate = DateTime.Now;


            _sliderRepository.Update(existedSlider);

            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "Slider ugurla modified olundu"
            };
        }
    }
}
