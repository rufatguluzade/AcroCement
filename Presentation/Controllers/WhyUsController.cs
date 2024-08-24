using Business.DTOs.Common;

using Business.DTOs.WhyUs.Request;
using Business.DTOs.WhyUs.Response;
using Business.Services.Abstraction;

using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhyUsController : ControllerBase
    {
        private readonly IWhyUsService _whyUsService;


        public WhyUsController(IWhyUsService whyUsService)
        {
            _whyUsService = whyUsService;
        }


        #region Documentation
        /// <summary>
        /// WhyUs yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]


        public async Task<Response> CreateAsync([FromForm] WhyUsCreateDTO model)
        {
            return await _whyUsService.CreateAsync(model);
        }


        #region Documentation
        /// <summary>
        /// WhyUs redakte etmek üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response> UpdateAsync(int id, [FromForm] WhyUsUpdateDTO model)
        {
            return await _whyUsService.UpdateAsync(id, model);
        }



        #region Documentation
        /// <summary>
        ///  WhyUs silinməsi
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Delete")]
        public async Task<Response> DeleteAsync(int id)
        {
            return await _whyUsService.DeleteAsync(id);
        }



        #region Documentation
        /// <summary>
        ///  WhyUs id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<WhyUsResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetById")]
        public async Task<Response<WhyUsResponseDTO>> GetAsync(int id)
        {
            return await _whyUsService.GetAsync(id);
        }



        #region Documentation
        /// <summary>
        /// WhyUs siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<WhyUsResponseDTO>>))]
        #endregion
        [HttpGet()]
        public async Task<Response<List<WhyUsResponseDTO>>> GetAllAsync()
        {
            return await _whyUsService.GetAllAsync();


        }
    }
}
