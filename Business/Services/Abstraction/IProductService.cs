using Business.DTOs.Common;
using Business.DTOs.Product.Request;
using Business.DTOs.Product.Response;

namespace Business.Services.Abstraction
{
    public interface IProductService
    {
        Task<Response> CreateAsync(ProductCreateDTO model);
        Task<Response> UpdateAsync(int id,ProductUpdateDTO model);
        Task<Response> DeleteAsync(int id);
        Task<Response<ProductResponseDTO>> GetAsync(int id);
        Task<Response<List<ProductResponseDTO>>> GetAllAsync(string? search);
    }
}
