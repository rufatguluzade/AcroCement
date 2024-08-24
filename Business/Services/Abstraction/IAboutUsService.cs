using Business.DTOs.AboutUs.Request;
using Business.DTOs.AboutUs.Response;
using Business.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstraction
{
    public interface IAboutUsService
    {
        Task<Response> CreateAsync(AboutUsCreateDTO model);
        Task<Response> UpdateAsync(int id,AboutUsUpdateDTO model);
        Task<Response<AboutUsResponseDTO>> GetAsync(int id);
        Task<Response<List<AboutUsResponseDTO>>> GetAllAsync();
        Task<Response> DeleteAsync(int id);
    }
}
