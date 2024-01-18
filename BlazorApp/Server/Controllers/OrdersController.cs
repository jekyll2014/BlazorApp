using BlazorApp.Server.Services.Interfaces;
using BlazorApp.Server.Services.QueryModels;
using BlazorApp.Shared.DTO;

using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IProjectOrdersService _service;

        public OrdersController(IProjectOrdersService service)
        {
            _service = service;
        }

        [HttpGet("GetByQuery")]
        public IEnumerable<OrderDto> GetByQuery([FromQuery] GetOrderQuery request)
        {
            return _service.Get(request);
        }

        [HttpGet("GetById/{id}")]
        public OrderDto GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpPost("Create")]
        public async Task<OrderDto> Create([FromBody] OrderDto create)
        {
            return await _service.CreateAsync(create);
        }

        [HttpPut("Update")]
        public async Task<OrderDto> Update([FromBody] OrderDto item)
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
