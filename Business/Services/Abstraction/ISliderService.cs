using Business.DTOs.Common;
using Business.DTOs.Slider.Request;
using Business.DTOs.Slider.Response;

namespace Business.Services.Abstraction
{
    public interface ISliderService
    {
        Task<Response> CreateAsync(SliderCreateDTO model);
        Task<Response> UpdateAsync(int id, SliderUpdateDTO model);
        Task<Response<SliderResponseDTO>> GetAsync(int id);

        Task<Response<List<SliderResponseDTO>>> GetAllAsync();

        Task<Response> DeleteAsync(int id);
    }
}
