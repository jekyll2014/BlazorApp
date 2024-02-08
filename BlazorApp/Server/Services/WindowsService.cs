using AutoMapper;

using BlazorApp.Server.DataBase.Repository.Interfaces;
using BlazorApp.Server.Models;
using BlazorApp.Server.Services.Interfaces;
using BlazorApp.Server.Services.QueryModels;
using BlazorApp.Shared.DTO;

using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Services
{
    public class WindowsService : IProjectWindowsService
    {
        private readonly IWindowsRepository _repository;
        private readonly IMapper _mapper;

        public WindowsService(IWindowsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<WindowDto> Get(GetWindowQuery request)
        {
            var query = _repository.GetByQuery();

            if (request.Id != null)
            {
                query = query.Where(n => n.Id == request.Id);
            }

            if (request.OrderId != null)
            {
                query = query.Where(n => n.OrderId == request.OrderId);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(n => n.Name == request.Name);
            }

            if (request.QuantityOfWindows != null)
            {
                query = query.Where(n => n.QuantityOfWindows == request.QuantityOfWindows);
            }

            if (!string.IsNullOrEmpty(request.Include))
            {
                query = query.Include(request.Include);
            }

            var result = query
                .Select(n => _mapper.Map<WindowDto>(n)).AsEnumerable();

            return result;
        }

        public async Task<WindowDto> GetById(int id)
        {
            return _mapper.Map<WindowDto>(await _repository.GetById(id));
        }

        public async Task<WindowDto> CreateAsync(WindowDto newItem)
        {
            var result = await _repository.CreateAsync(_mapper.Map<Window>(newItem));

            return _mapper.Map<WindowDto>(result);
        }

        public async Task<WindowDto> UpdateAsync(WindowDto item)
        {
            var result = await _repository.UpdateAsync(_mapper.Map<Window>(item));

            return _mapper.Map<WindowDto>(result);
        }

        public void DeleteAsync(int id)
        {
            _repository.DeleteAsync(id);
        }
    }
}
