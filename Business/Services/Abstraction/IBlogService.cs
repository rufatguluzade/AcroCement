﻿using Business.DTOs.Blog.Request;
using Business.DTOs.Blog.Response;
using Business.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstraction
{
    public interface IBlogService
    {
        Task<Response> CreateAsync(BlogCreateDTO model);
        Task<Response> UpdateAsync(int id, BlogUpdateDTO model);
        Task<Response> DeleteAsync(int id);
        Task<Response<BlogResponseDTO>> GetAsync(int id);
        Task<Response<List<BlogResponseDTO>>> GetAllAsync(string? search);
    }
}
