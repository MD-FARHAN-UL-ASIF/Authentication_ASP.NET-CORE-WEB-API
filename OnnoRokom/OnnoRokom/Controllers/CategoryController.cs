using System.Net;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.IServices;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnnoRokom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService; // Change to interface

        public CategoryController(ICategoryService categoryService) // Change to interface
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.Get();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await _categoryService.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            var response = await _categoryService.Create(categoryDTO);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            categoryDTO.Id = id;

            if (categoryDTO == null)
            {
                return BadRequest(new Response(HttpStatusCode.BadRequest, "Invalid category ID"));
            }


            var response = await _categoryService.Update(categoryDTO);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.Delete(id);

            if (response.Status == HttpStatusCode.OK.ToString())
            {
                return Ok(response);
            }
            else if (response.Status == HttpStatusCode.NotFound.ToString())
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
