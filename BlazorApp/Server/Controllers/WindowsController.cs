using BlazorApp.Server.Services.Interfaces;
using BlazorApp.Server.Services.QueryModels;
using BlazorApp.Shared.DTO;

using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WindowsController : ControllerBase
    {
        private readonly IProjectWindowsService _service;

        public WindowsController(IProjectWindowsService service)
        {
            _service = service;
        }

        [HttpGet("GetByQuery")]
        public IEnumerable<WindowDto> GetByQuery([FromQuery] GetWindowQuery request)
        {
            return _service.Get(request);
        }

        [HttpGet("GetById/{id}")]
        public WindowDto GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost("Create")]
        public async Task<WindowDto> Create([FromBody] WindowDto create)
        {
            return await _service.CreateAsync(create);
        }

        [HttpPut("Update")]
        public async Task<WindowDto> Update([FromBody] WindowDto item)
        {
            return await _service.UpdateAsync(item);
        }

        [HttpDelete("Delete/{id}")]
        public void Delete(int id)
        {
            _service.DeleteAsync(id);
        }
    }
}
