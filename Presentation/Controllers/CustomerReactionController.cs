using Business.DTOs.Common;
using Business.DTOs.CustomerReaction.Request;
using Business.DTOs.CustomerReaction.Response;
using Business.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReactionController : ControllerBase
    {
        private readonly ICustomerReactionService _customerReactionService;

        public CustomerReactionController(ICustomerReactionService customerReactionService)
        { 
            _customerReactionService = customerReactionService;
        }



        #region Documentation
        /// <summary>
        ///  CustomerReaction yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]
        public async Task<Response> CreateAsync([FromBody] CustomerReactionCreateDTO model)
        {
            return await _customerReactionService.CreateAsync(model);
        }


        #region Documentation
        /// <summary>
        ///  CustomerReaction redaktə olunması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response> UpdateAsync(int id, [FromForm] CustomerReactionUpdateDTO model)
        {
            return await _customerReactionService.UpdateAsync(id, model);
        }



        #region Documentation
        /// <summary>
        ///   CustomerReaction silinməsi  
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Delete")]
        public async Task<Response> DeleteAsync(int id)
        {
            return await _customerReactionService.DeleteAsync(id);
        }




        #region Documentation
        /// <summary>
        ///   CustomerReaction id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<CustomerReactionResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetById")]
        public async Task<Response<CustomerReactionResponseDTO>> GetAsync(int id)
        {
            return await _customerReactionService.GetAsync(id);
        }



        #region Documentation
        /// <summary>
        ///  CustomerReaction siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<CustomerReactionResponseDTO>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetAll")]
        public async Task<Response<List<CustomerReactionResponseDTO>>> GetAllAsync()
        {
            return await _customerReactionService.GetAllAsync();
        }

    }
}
