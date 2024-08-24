using AutoMapper;
using Business.DTOs.Blog.Request;
using Business.DTOs.Blog.Response;
using Business.DTOs.Common;
using Business.Exceptions;
using Business.Extensions;
using Business.Helpers;
using Business.Services.Abstraction;
using Business.Validators.Blog;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concered
{
    public class BlogService : IBlogService
    {

        private readonly IBlogRepository _blogRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITagRepository _tagRepository;

        private readonly IWebHostEnvironment _env;


        public BlogService(IBlogRepository blogRepository, IUnitOfWork unitOfWork , IMapper mapper, IWebHostEnvironment env, ITagRepository tagRepository)
        {
            _blogRepository = blogRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
            _tagRepository = tagRepository;

        }

        public async Task<Response> CreateAsync(BlogCreateDTO model)
        {
            var result = await new BlogCreateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }


            var blog = _mapper.Map<Blog>(model);

            List<BlogTag> blogTags = new List<BlogTag>();



            foreach (int tagId in blog.TagIds)
            {
                if (blog.TagIds.Where(t => t == tagId).Count() > 1)
                {
                    throw new ValidationException("Bir tagdan bir defe secilmelidir");
                }

                if (!await _tagRepository.IsExistAsync(t => t.Id == tagId))
                {
                    throw new ValidationException("secilen tag yalnisdir");
                }

                BlogTag blogTag = new BlogTag
                {
                    CreatedDate = DateTime.UtcNow,


                    TagId = tagId

                };

                //taglari bos liste add etdik
                blogTags.Add(blogTag);
            }


            if (blog.ImageFile == null)
            {

                throw new ValidationException("Image daxil edilmelidir");
            }



            if (!blog.ImageFile.CheckFileSize(1000))
            {

                throw new ValidationException("Image olcusu 1 mb cox olmamalidir");
            }


            if (!blog.ImageFile.CheckFileType("image/jpeg"))
            {


                throw new ValidationException("Image jpg tipi olmalidir");

            }


            blog.ImageUrl = blog.ImageFile.CreateImage(_env, "img", "blog");
            blog.BlogTags = blogTags;



            await _blogRepository.CreateAsync(blog);
            await _unitOfWork.CommitAsync();







            return new Response
            {

                Message = "blog ugurla yaradildi"
            };

        }

        public async Task<Response> DeleteAsync(int id)
        {
            var blog = await _blogRepository.GetAsync(id);

            if (blog is null)
            {
                throw new NotFoundException("blog tapilmadi");
            }


            _blogRepository.Delete(blog);
            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "blog ugurla silindi"
            };
        }

        public async Task<Response<List<BlogResponseDTO>>> GetAllAsync(string? search)
        {

            var blogs = await _blogRepository.GetFiltered(
          b => search != null ? b.Title.Contains(search) : true,
          isTracking: false,
          includes: new[] { "BlogTags.Tag" }
      ).ToListAsync();




            if (blogs is null)
            {
                throw new NotFoundException("blog tapilmadi");
            }


            return new Response<List<BlogResponseDTO>>
            {
                Data = _mapper.Map<List<BlogResponseDTO>>(blogs),
                Message = "ugurlu alindi"
            };
        }

        public async Task<Response<BlogResponseDTO>> GetAsync(int id)
        {
            var blog = await _blogRepository.GetSingleAsync(b => b.Id == id, "BlogTags.Tag");

            if (blog is null)
            {
                throw new NotFoundException("Blog tapilmadi");
            }



            return new Response<BlogResponseDTO>
            {
                Data = _mapper.Map<BlogResponseDTO>(blog),
                Message = "blog ugurla getirildi"
            };

        }

        public async Task<Response> UpdateAsync(int id, BlogUpdateDTO model)
        {
            var result = await new BlogUpdateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }


           var existBlog = await _blogRepository.GetAsync(id);
         

            if (existBlog is null)
            {
                throw new NotFoundException("blog tapilmadi");
            }
            _mapper.Map(model, existBlog);


            await _blogRepository.RemoveBlogTagsAsync(existBlog.Id);

            List<BlogTag> blogTags = new List<BlogTag>();

            
            

      
            foreach (int tagId in model.TagIds)
            {
                if (model.TagIds.Where(t => t == tagId).Count() > 1)
                {

                    throw new ValidationException("Bir tagdan bir defe secilmelidir");

                }




                    if (!await _tagRepository.IsExistAsync(t => t.Id == tagId))
                {

                    throw new ValidationException("secilen tag yalnisdir");

                }

                BlogTag blogTag = new BlogTag
                {
                    CreatedDate = DateTime.UtcNow,


                    TagId = tagId

                };

                //taglari bos liste add etdik
                blogTags.Add(blogTag);
            }

        
   


            if (model.ImageFile == null)
            {

                throw new ValidationException("Image daxil edilmelidir");
            }



            if (!model.ImageFile.CheckFileSize(1000))
            {

                throw new ValidationException("Image olcusu 1 mb cox olmamalidir");
            }


            if (!model.ImageFile.CheckFileType("image/jpeg"))
            {


                throw new ValidationException("Image jpg tipi olmalidir");

            }



            Helper.DeleteFile(_env, existBlog.ImageUrl, "img", "blog");
            existBlog.ImageUrl = model.ImageFile.CreateImage(_env, "img", "blog");
            existBlog.BlogTags = blogTags;
            existBlog.ModifiedDate = DateTime.Now;




            try
            {

                _blogRepository.Update(existBlog);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                int a = 32;
            }



            return new Response
            {
                Message = "blog uğurla redaktə olundu"
            };

        }
    }
}
