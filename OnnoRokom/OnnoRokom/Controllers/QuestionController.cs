using BLL.DTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace OnnoRokom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var response = await _questionService.Get();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var response = await _questionService.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionDTO questionDTO)
        {
            var response = await _questionService.Create(questionDTO);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionDTO questionDTO)
        {
            questionDTO.Id = id;

            if (questionDTO == null)
            {
                return BadRequest(new Response(HttpStatusCode.BadRequest, "Invalid question ID"));
            }

            var response = await _questionService.Update(questionDTO);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var response = await _questionService.Delete(id);

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

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetQuestionsByUserId(int userId)
        {
            var response = await _questionService.GetByUserId(userId);
            return Ok(response);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetQuestionsByCategoryId(int categoryId)
        {
            var response = await _questionService.GetByCategoryId(categoryId);
            return Ok(response);
        }

    }
}
