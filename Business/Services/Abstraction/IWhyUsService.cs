using Business.DTOs.Common;
using Business.DTOs.WhyUs.Request;
using Business.DTOs.WhyUs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstraction
{
    public interface IWhyUsService
    {
        Task<Response> CreateAsync(WhyUsCreateDTO model);
        Task<Response> UpdateAsync(int id, WhyUsUpdateDTO model);
        Task<Response<WhyUsResponseDTO>> GetAsync(int id);

        Task<Response<List<WhyUsResponseDTO>>> GetAllAsync();

        Task<Response> DeleteAsync(int id);
    }
}
