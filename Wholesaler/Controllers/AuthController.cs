﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Wholesaler.DataTransferObject;
using Wholesaler.Services;

namespace Wholesaler.Controllers
{
    [Route("api/v1/auth")]
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
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto userDto)
        {
            await _authService.RegisterUser(userDto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserDto userDto)
        {
            if(!await _authService.CheckUserExists(userDto))
            {
                return BadRequest($"User {userDto.Email} doesn't exist");
            }

            return Ok(await _authService.GenerateJwt(userDto));
        }
    }
}
