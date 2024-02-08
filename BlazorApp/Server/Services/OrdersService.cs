using AutoMapper;

using BlazorApp.Server.DataBase.Repository.Interfaces;
using BlazorApp.Server.Models;
using BlazorApp.Server.Services.Interfaces;
using BlazorApp.Server.Services.QueryModels;
using BlazorApp.Shared.DTO;

using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Services
{
    public class OrdersService : IProjectOrdersService
    {
        private readonly IOrdersRepository _repository;
        private readonly IMapper _mapper;

        public OrdersService(IOrdersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<OrderDto> Get(GetOrderQuery request)
        {
            var query = _repository.GetByQuery();

            if (request.Id != null)
            {
                query = query.Where(n => n.Id == request.Id);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(n => n.Name == request.Name);
            }

            if (!string.IsNullOrEmpty(request.State))
            {
                query = query.Where(n => n.State == request.State);
            }

            if (!string.IsNullOrEmpty(request.Include))
            {
                query = query.Include(request.Include);
            }

            var result = query
                .Select(n => _mapper.Map<OrderDto>(n)).AsEnumerable();

            return result;
        }

        public async Task<OrderDto> GetById(int id)
        {
            var result = await _repository.GetById(id);

            return _mapper.Map<OrderDto>(result);
        }

        public async Task<OrderDto> CreateAsync(OrderDto newItem)
        {
            var result = await _repository.CreateAsync(_mapper.Map<Order>(newItem));

            return _mapper.Map<OrderDto>(result);
        }

        public async Task<OrderDto> UpdateAsync(OrderDto item)
        {
            var result = await _repository.UpdateAsync(_mapper.Map<Order>(item));

            return _mapper.Map<OrderDto>(result);
        }

        public void DeleteAsync(int id)
        {
            _repository.DeleteAsync(id);
        }
    }
}
