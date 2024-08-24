
using Business.DTOs.Category.Request;
using Business.DTOs.Category.Response;
using Business.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstraction
{
    public interface ICategoryService
    {
        Task<Response> CreateAsync(CategoryCreateDTO model);
        Task<Response> UpdateAsync(int id, CategoryUpdateDTO model);
        Task<Response> DeleteAsync(int id);
        Task<Response<CategoryResponseDTO>> GetAsync(int id);
        Task<Response<List<CategoryResponseDTO>>> GetAllAsync();
        Task<Response<CategoryWithProductDTO>> GetCategoryProductAllAsync(int id);

    }
}
