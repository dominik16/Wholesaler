using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wholesaler.DataTransferObject;
using Wholesaler.Services;

namespace Wholesaler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(CreateUserDto userDto)
        {
            await _authService.RegisterUser(userDto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserDto userDto)
        {
            if(!await _authService.CheckUserExists(userDto))
            {
                return BadRequest($"User {userDto.Email} doesn't exist");
            }

            return Ok(await _authService.GenerateJwt(userDto));
        }
    }
}
