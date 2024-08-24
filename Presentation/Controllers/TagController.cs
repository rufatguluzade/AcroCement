using Business.DTOs.Common;
using Business.DTOs.Tag.Request;
using Business.DTOs.Tag.Response;
using Business.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;


        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }


        #region Documentation
        /// <summary>
        /// Tag yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]
  

        public async Task<Response> CreateAsync([FromBody] TagCreateDTO model)
        {
            return await _tagService.CreateAsync(model);
        }


        #region Documentation
        /// <summary>
        /// Tag redakte etmek üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response> UpdateAsync(int id, [FromBody] TagUpdateDTO model)
        {
            return await _tagService.UpdateAsync(id, model);
        }



        #region Documentation
        /// <summary>
        ///  tag silinməsi
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Delete")]
        public async Task<Response> DeleteAsync(int id)
        {
            return await _tagService.DeleteAsync(id);
        }



        #region Documentation
        /// <summary>
        ///  tag id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<TagResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetById")]
        public async Task<Response<TagResponseDTO>> GetAsync(int id)
        {
            return await _tagService.GetAsync(id);
        }



        #region Documentation
        /// <summary>
        /// tag siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<TagResponseDTO>>))]
        #endregion
        [HttpGet()]
        public async Task<Response<List<TagResponseDTO>>> GetAllAsync()
        {
            return await _tagService.GetAllAsync();


        }
    }
}
