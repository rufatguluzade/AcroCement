using Business.DTOs.Category.Request;
using Business.DTOs.Category.Response;
using Business.DTOs.Common;
using Business.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region Documentation
        /// <summary>
        /// Category siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<CategoryResponseDTO>>))]
        #endregion
        [HttpGet()]
        public async Task<Response<List<CategoryResponseDTO>>> GetAllAsync()
        {
            return await _categoryService.GetAllAsync();
        }


        #region Documentation
        /// <summary>
        /// Categorya aid productlarin siyahısını götürmək üçün
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<CategoryWithProductDTO>>))]
        #endregion
        [HttpGet("GetProductByCategory")]
        public async Task<Response<CategoryWithProductDTO>> GetCategoryProductAllAsync(int id)
        {
            return await _categoryService.GetCategoryProductAllAsync(id);
        }



        #region Documentation
        /// <summary>
        ///  Category id parametrinə görə götürülməsi üçün
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<CategoryResponseDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpGet("GetById")]
        public async Task<Response<CategoryResponseDTO>> GetAsync(int id)
        {
            return await _categoryService.GetAsync(id);
        }

        #region Documentation
        /// <summary>
        /// category yaradılması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPost("Create")]
        public async Task<Response> CreateAsync([FromBody] CategoryCreateDTO model)
        {
            return await _categoryService.CreateAsync(model);
        }

        #region Documentation
        /// <summary>
        /// category redaktə olunması üçün
        /// </summary>
        /// <param name="model"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        #endregion
        [HttpPut("Update")]
        public async Task<Response> UpdateAsync(int id, [FromBody] CategoryUpdateDTO model)
        {
            return await _categoryService.UpdateAsync(id, model);
        }

        #region Documentation
        /// <summary>
        ///  Category silinməsi
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        #endregion
        [HttpDelete("Delete")]
        public async Task<Response> DeleteAsync(int id)
        {
            return await _categoryService.DeleteAsync(id);
        }
    }
}
