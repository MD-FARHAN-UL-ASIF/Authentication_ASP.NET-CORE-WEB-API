using BLL.DTOs;
using BLL.IServices;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace OnnoRokom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnswers()
        {
            var response = await _answerService.Get();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            var response = await _answerService.GetById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] AnswerDTO answerDTO)
        {
            var response = await _answerService.Create(answerDTO);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(int id, [FromBody] AnswerDTO answerDTO)
        {
            answerDTO.Id = id;

            if (answerDTO == null)
            {
                return BadRequest(new Response(HttpStatusCode.BadRequest, "Invalid Answer ID"));
            }

            var response = await _answerService.Update(answerDTO);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var response = await _answerService.Delete(id);

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
        public async Task<IActionResult> GetAnswersByUserId(int userId)
        {
            var response = await _answerService.GetByUserId(userId);
            return Ok(response);
        }

        [HttpGet("question/{questionId}")]
        public async Task<IActionResult> GetAnswersByQuestionId(int questionId)
        {
            var response = await _answerService.GetByQuestionId(questionId);
            return Ok(response);
        }
    }
}
