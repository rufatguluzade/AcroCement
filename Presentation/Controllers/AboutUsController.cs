using Business.DTOs.AboutUs.Request;
using Business.DTOs.AboutUs.Response;
using Business.DTOs.Common;
using Business.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutUsController : ControllerBase
    {

        private readonly IAboutUsService _aboutUsService;

        public AboutUsController(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }

        #region Documentation
        /// <summary>
        /// aboutUs yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]
        public async Task<Response> CreateAsync([FromForm] AboutUsCreateDTO model)
        {
            return await _aboutUsService.CreateAsync(model);
        }


        #region Documentation
        /// <summary>
        /// aboutUs redaktə olunması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response> UpdateAsync(int id, [FromForm] AboutUsUpdateDTO model)
        {
            return await _aboutUsService.UpdateAsync(id, model);
        }



        #region Documentation
        /// <summary>
        ///  aboutUs silinməsi  
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Delete")]
        public async Task<Response> DeleteAsync(int id)
        {
            return await _aboutUsService.DeleteAsync(id);
        }




        #region Documentation
        /// <summary>
        ///  aboutUs id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<AboutUsResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetById")]


        public async Task<Response<AboutUsResponseDTO>> GetAsync(int id)
        {
            return await _aboutUsService.GetAsync(id);
        }



        #region Documentation
        /// <summary>
        /// aboutUs siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<AboutUsResponseDTO>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetAll")]


        public async Task<Response<List<AboutUsResponseDTO>>> GetAllAsync()
        {
            return await _aboutUsService.GetAllAsync();
        }

    }
}
