using BLL.DTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace OnnoRokom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            // Check if the user has the Admin role
            if (!User.IsInRole("Admin"))
            {
                // Return custom response with 403 status code and message
                return StatusCode(StatusCodes.Status403Forbidden, new Response(HttpStatusCode.Forbidden, "Only Admin has Access"));
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _userService.GetAllUser();
            return Ok(new Response(HttpStatusCode.OK, "Success", response));
        }



        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _userService.GetUser(userID);
            return Ok(response);
        }
    }
}
