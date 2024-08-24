using AutoMapper;
using Business.DTOs.Blog.Response;
using Business.DTOs.Common;
using Business.DTOs.Contact.Response;
using Business.DTOs.Product.Request;
using Business.DTOs.Product.Response;
using Business.Exceptions;
using Business.Extensions;
using Business.Helpers;
using Business.Services.Abstraction;
using Business.Validators.Product;
using Common.Entities;
using DataAccess.Migrations;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Business.Services.Concered
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;


        private readonly IWebHostEnvironment _env;

        public ProductService(ICategoryRepository categoryRepository ,IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper , IWebHostEnvironment env, IFileService fileService)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _fileService = fileService;
            _categoryRepository = categoryRepository;

        }
        public async Task<Response> CreateAsync(ProductCreateDTO model)
        {
            var result = await new ProductCreateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var product = _mapper.Map<Product>(model);

            if (!await _categoryRepository.IsExistAsync(c => c.Id == product.CategoryId))
            {
                throw new ValidationException("gelen category yalnisdir");
            }


            if (model.ProductImageFiles is not null)
            {
                List<ProductImage> files = new List<ProductImage>();

                foreach (var file in model.ProductImageFiles)
                {
                    string image = await _fileService.FileUploadAsync(file,
                   Path.Combine("img", "product"), "image/jpeg", 3000);
                    files.Add(new ProductImage { Image = image });
                };

                product.ProductImages = files;
            }



            await _productRepository.CreateAsync(product);
            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "product ugurla yaradildi"
            };
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);

            if (product is null)
            {
                throw new NotFoundException("product tapilmadi");
            }


            _productRepository.Delete(product);
            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "product ugurla silindi"
            };
        }

        public async Task<Response<List<ProductResponseDTO>>> GetAllAsync(string? search)
        {
            var products = await _productRepository.GetFiltered(
              b => search != null ? b.Title.Contains(search) : true,
              isTracking: false,
              includes: new[] {"ProductImages","Category"}
          ).ToListAsync();




            if (products is null)
            {
                throw new NotFoundException("data tapilmadi");
            }


            return new Response<List<ProductResponseDTO>>
            {
                Data = _mapper.Map<List<ProductResponseDTO>>(products),
                Message = "ugurlu alindi"
            };

        }

        public async Task<Response<ProductResponseDTO>> GetAsync(int id)
        {
            var product = await _productRepository.GetSingleAsync(b => b.Id == id, "ProductImages", "Category");

            if (product is null)
                throw new NotFoundException($"The product with ID {id} was not found");


            return new Response<ProductResponseDTO>
            {
                Data = _mapper.Map<ProductResponseDTO>(product),
                Message = "data ugurla getirildi"
            };
        }

        public async Task<Response> UpdateAsync(int id, ProductUpdateDTO model)
        {

            var result = await new ProductUpdateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var existedProduct = await _productRepository.GetSingleAsync(b => b.Id == id,  "ProductImages");

            if (existedProduct is null)
                throw new NotFoundException($"No product found with the ID {id}");


            if (!await _categoryRepository.IsExistAsync(c => c.Id == existedProduct.CategoryId))
            {
                throw new ValidationException("gelen category yalnisdir");
            }

            _mapper.Map(model, existedProduct);



            if (model.ProductImageFiles is not null)
            {
                List<ProductImage> files = new List<ProductImage>();

                foreach (var file in model.ProductImageFiles)
                {
                    string image = await _fileService.FileUploadAsync(file,
                   Path.Combine("img", "product"), "image/jpeg", 3000);
                    files.Add(new ProductImage { Image = image });
                };

                existedProduct.ProductImages = files;
            }





            existedProduct.CategoryId = existedProduct.CategoryId;
            existedProduct.ModifiedDate = DateTime.Now;
            _productRepository.Update(existedProduct);

            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "product ugurla modified olundu"
            };
        }
    }
}
