using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wholesaler.Services;
using Wholesaler.DataTransferObject;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace Wholesaler.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")]
    public class ManageUserController : ControllerBase
    {
        private readonly IManageService _service;

        public ManageUserController(IManageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            return Ok(await _service.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers([FromRoute] int id)
        {
            return Ok(await _service.GetUser(id));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if(!await _service.CheckExistsUser(id))
            {
                return BadRequest($"User with id: {id} doesn't exists");
            }

            await _service.DeleteUser(id);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeUserRole([FromRoute] int userId, [FromRoute] int roleId)
        {
            if (!await _service.CheckExistsUser(userId))
            {
                return BadRequest($"User with id: {userId} doesn't exists");
            }

            if (!await _service.CheckRoleExists(roleId))
            {
                return BadRequest($"Role with id: {roleId} doesn't exists");
            }

            await _service.ChangeRole(userId, roleId);
            return Ok();
        }
    }
}
