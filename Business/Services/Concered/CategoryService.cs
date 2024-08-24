using AutoMapper;
using Business.DTOs.Category.Request;
using Business.DTOs.Category.Response;
using Business.DTOs.Common;
using Business.DTOs.Slider.Response;
using Business.Exceptions;
using Business.Services.Abstraction;
using Business.Validators.Category;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Concered
{
    public class CategoryService : ICategoryService
    {


        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
           _categoryRepository = categoryRepository;    
            _unitOfWork = unitOfWork;
            _mapper = mapper;



        }


        public async Task<Response> CreateAsync(CategoryCreateDTO model)
        {
            var result = await new CategoryCreateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            if (await _categoryRepository.IsExistAsync(m => m.Name == model.Name))
            {
                throw new ValidationException("bu category movcuddur");
            }

            var category = _mapper.Map<Category>(model);

            await _categoryRepository.CreateAsync(category);
            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "category ugurla yaradildi"
            };

        }

        public async Task<Response> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category is null)
            {
          
                throw new NotFoundException("category tapilmadi");
            }

            _categoryRepository.Delete(category);
            await _unitOfWork.CommitAsync();



        
            return new Response
            {
                Message = "Category uğurla silindi"
            };
        }

        public async Task<Response<List<CategoryResponseDTO>>> GetAllAsync()
        {
            var response = await _categoryRepository.GetAllAsync();

            if (response == null)
            {

                throw new NotFoundException("category tapilmadi");
            }



            return new Response<List<CategoryResponseDTO>>
            {
                Data = _mapper.Map<List<CategoryResponseDTO>>(response),
                Message = "Category ugurla getirildi"
            };
        }

        public async Task<Response<CategoryResponseDTO>> GetAsync(int id)
        {
            var response = await _categoryRepository.GetAsync(id);
            if (response == null)
            {
                throw new NotFoundException("Category tapilmadi");
            }


            return new Response<CategoryResponseDTO>
            {
                Data = _mapper.Map<CategoryResponseDTO>(response),
                Message = "data ugurla getirildi"
            };
        }

        public async Task<Response<CategoryWithProductDTO>> GetCategoryProductAllAsync(int id)
        {
            var response = await _categoryRepository.GetSingleAsync(b => b.Id == id, "Products.ProductImages");

            if (response is null)
            {

                throw new NotFoundException("Hec bir response tapilmadi");
            }

            return new Response<CategoryWithProductDTO>
            {
                Data = _mapper.Map<CategoryWithProductDTO>(response),
                Message = "data ugurla getirildi"
            };
        }

        public async Task<Response> UpdateAsync(int id, CategoryUpdateDTO model)
        {
            var result = await new CategoryUpdateDTOValidator().ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            var existCategory = await _categoryRepository.GetAsync(id);
            if (existCategory is null)
            {
                throw new NotFoundException("category tapılmadı");
            }
            if (await _categoryRepository.IsExistAsync(m => m.Name == model.Name))
            {
                throw new ValidationException("bu adda category movcuddur");
            }

        

            _mapper.Map(model, existCategory);

            existCategory.Name = model.Name;
            existCategory.ModifiedDate = DateTime.Now;

            _categoryRepository.Update(existCategory);
            await _unitOfWork.CommitAsync();



        
            return new Response
            {
                Message = "category uğurla redaktə olundu"
            };

        }
    }
}
