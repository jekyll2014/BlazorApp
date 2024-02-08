using AutoMapper;

using BlazorApp.Server.DataBase.Repository.Interfaces;
using BlazorApp.Server.Models;
using BlazorApp.Server.Services.Interfaces;
using BlazorApp.Server.Services.QueryModels;
using BlazorApp.Shared.DTO;

using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.Services
{
    public class SubElementsService : IProjectSubElementsService
    {
        private readonly ISubElementsRepository _repository;
        private readonly IMapper _mapper;

        public SubElementsService(ISubElementsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<SubElementDto> Get(GetSubElementQuery request)
        {
            var query = _repository.GetByQuery();

            if (request.Id != null)
            {
                query = query.Where(n => n.Id == request.Id);
            }

            if (request.WindowId != null)
            {
                query = query.Where(n => n.WindowId == request.WindowId);
            }

            if (request.Element != null)
            {
                query = query.Where(n => n.Element == request.Element);
            }

            if (!string.IsNullOrEmpty(request.Type))
            {
                query = query.Where(n => n.Type == request.Type);
            }

            if (request.Width != null)
            {
                query = query.Where(n => n.Width == request.Width);
            }

            if (request.Height != null)
            {
                query = query.Where(n => n.Height == request.Height);
            }

            if (!string.IsNullOrEmpty(request.Include))
            {
                query = query.Include(request.Include);
            }

            var result = query
                .Select(n => _mapper.Map<SubElementDto>(n)).AsEnumerable();

            return result;
        }

        public async Task<SubElementDto> GetById(int id)
        {
            return _mapper.Map<SubElementDto>(await _repository.GetById(id));
        }

        public async Task<SubElementDto> CreateAsync(SubElementDto newItem)
        {
            var result = await _repository.CreateAsync(_mapper.Map<SubElement>(newItem));

            return _mapper.Map<SubElementDto>(result);
        }

        public async Task<SubElementDto> UpdateAsync(SubElementDto item)
        {
            var result = await _repository.UpdateAsync(_mapper.Map<SubElement>(item));

            return _mapper.Map<SubElementDto>(result);
        }

        public void DeleteAsync(int id)
        {
            _repository.DeleteAsync(id);
        }
    }
}
