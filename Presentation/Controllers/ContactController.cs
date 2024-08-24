
using Business.DTOs.Common;
using Business.DTOs.Contact.Request;
using Business.DTOs.Contact.Response;
using Business.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }



        #region Documentation
        /// <summary>
        ///  Contact yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]
        public async Task<Response> CreateAsync([FromForm] ContactCreateDTO model)
        {
            return await _contactService.CreateAsync(model);
        }


        #region Documentation
        /// <summary>
        ///  Contact redaktə olunması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response> UpdateAsync(int id, [FromForm] ContactUpdateDTO model)
        {
            return await _contactService.UpdateAsync(id, model);
        }



        #region Documentation
        /// <summary>
        ///   Contact silinməsi  
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Delete")]
        public async Task<Response> DeleteAsync(int id)
        {
            return await _contactService.DeleteAsync(id);
        }




        #region Documentation
        /// <summary>
        ///   Contact id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ContactResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetById")]
        public async Task<Response<ContactResponseDTO>> GetAsync(int id)
        {
            return await _contactService.GetAsync(id);
        }



        #region Documentation
        /// <summary>
        ///  Contact siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<ContactResponseDTO>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetAll")]
        public async Task<Response<List<ContactResponseDTO>>> GetAllAsync()
        {
            return await _contactService.GetAllAsync();
        }


    }
}
