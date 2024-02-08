using BlazorApp.Server.Services.Interfaces;
using BlazorApp.Server.Services.QueryModels;
using BlazorApp.Shared.DTO;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubElementsController : ControllerBase
    {
        private readonly IProjectSubElementsService _service;

        public SubElementsController(IProjectSubElementsService service)
        {
            _service = service;
        }

        [HttpGet("GetByQuery")]
        public IEnumerable<SubElementDto> GetByQuery([FromQuery] GetSubElementQuery request)
        {
            return _service.Get(request);
        }

        [HttpGet("GetById/{id}")]
        public async Task<SubElementDto> GetById(int id)
        {
            return await _service.GetById(id);
        }

        [HttpPost("Create")]
        public async Task<SubElementDto> Create([FromBody] SubElementDto create)
        {
            return await _service.CreateAsync(create);
        }

        [HttpPut("Update")]
        public async Task<SubElementDto> Update([FromBody] SubElementDto item)
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
