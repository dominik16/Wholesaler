using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wholesaler.DataTransferObject;
using Wholesaler.Models;
using Wholesaler.Services;

namespace Wholesaler.Controllers
{
    [Route("api/v1/storages")]
    [ApiController]
    [Authorize]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _service;

        public StorageController(IStorageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStorages()
        {
            return Ok(await _service.GetAllStorages());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStorage([FromRoute] int id)
        {
            if(!await _service.CheckStorageExist(id))
            {
                return NotFound($"Storage with id {id} does not exist");
            }

            return Ok(await _service.GetSingleStorage(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AddStorage([FromBody] CreateStorageDto storage)
        {
            await _service.AddStorage(storage);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> UpdateStorage([FromRoute] int id, [FromBody] CreateStorageDto storage)
        {
            if (!await _service.CheckStorageExist(id))
            {
                return NotFound($"Storage with id {id} does not exist");
            }

            await _service.UpdateStorage(id, storage);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteStorage([FromRoute] int id)
        {
            if (!await _service.CheckStorageExist(id))
            {
                return NotFound($"Storage with id {id} does not exist");
            }

            await _service.DeleteStorage(id);
            return Ok();
        }
    }
}
