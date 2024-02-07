using BLL.DTOs;
using BLL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OnnoRokom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDTO userRegistrationDTO, string roleName)
        {
            // Call the Register method with the userRegistrationDTO and roleName
            var response = await _userService.Register(userRegistrationDTO, roleName);

            // Return the response
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            var response = await _userService.Login(loginDTO);
            return Ok(response);
        }
    }
}
