using Business.DTOs.Common;
using Business.DTOs.Slider.Request;
using Business.DTOs.Slider.Response;
using Business.DTOs.Tag.Request;
using Business.DTOs.Tag.Response;
using Business.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;


        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService; 
        }


        #region Documentation
        /// <summary>
        /// Slider yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]


        public async Task<Response> CreateAsync([FromForm] SliderCreateDTO model)
        {
            return await _sliderService.CreateAsync(model);
        }


        #region Documentation
        /// <summary>
        /// Slider redakte etmek üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response> UpdateAsync(int id, [FromForm] SliderUpdateDTO model)
        {
            return await _sliderService.UpdateAsync(id, model);
        }



        #region Documentation
        /// <summary>
        ///  Slider silinməsi
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Delete")]
        public async Task<Response> DeleteAsync(int id)
        {
            return await _sliderService.DeleteAsync(id);
        }



        #region Documentation
        /// <summary>
        ///  Slider id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<SliderResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetById")]
        public async Task<Response<SliderResponseDTO>> GetAsync(int id)
        {
            return await _sliderService.GetAsync(id);
        }



        #region Documentation
        /// <summary>
        /// Slider siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<SliderResponseDTO>>))]
        #endregion
        [HttpGet()]
        public async Task<Response<List<SliderResponseDTO>>> GetAllAsync()
        {
            return await _sliderService.GetAllAsync();


        }
    }
}
