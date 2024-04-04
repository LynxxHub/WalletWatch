using Microsoft.AspNetCore.Mvc;
using WalletWatchAPI.DTOs;
using WalletWatchAPI.Services.Interfaces;

namespace WalletWatchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionCategoryDTO>> GetCategoryAsync(string id)
        {
            return await _categoryService.GetCategoryByIdAsync(id);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(string id, TransactionCategoryDTO transactionCategoryDTO)
        {
            bool isUpdated;
            try
            {
                isUpdated = await _categoryService.UpdateCategoryAsync(id, transactionCategoryDTO);
            }
            catch (InvalidOperationException ex)
            {

                return NotFound(ex.Message);
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(TransactionCategoryDTO categoryDTO)
        {
            try
            {
                categoryDTO = await _categoryService.AddCategoryAsync(categoryDTO);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }

            return CreatedAtAction("GetCategory", new { id = categoryDTO.Id }, categoryDTO);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(string id)
        {
            bool isDeleted;

            try
            {
                isDeleted = await _categoryService.DeleteCategoryAsync(id);
            }
            catch (InvalidOperationException ex)
            {

                return NotFound(ex.Message);
            }


            return NoContent();
        }
    }
}