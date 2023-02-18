using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wholesaler.DataTransferObject;
using Wholesaler.Models;
using Wholesaler.Services;

namespace Wholesaler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetStorage(int id)
        {
            if(!await _service.CheckStorageExist(id))
            {
                return NotFound($"Storage with id {id} does not exist");
            }

            return Ok(await _service.GetSingleStorage(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddStorage(CreateStorageDto storage)
        {
            await _service.AddStorage(storage);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStorage(int id, CreateStorageDto storage)
        {
            if (!await _service.CheckStorageExist(id))
            {
                return NotFound($"Storage with id {id} does not exist");
            }

            await _service.UpdateStorage(id, storage);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStorage(int id)
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
