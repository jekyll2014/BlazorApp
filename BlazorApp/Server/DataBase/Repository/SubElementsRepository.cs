using AutoMapper;

using BlazorApp.Exceptions;
using BlazorApp.Server.DataBase.Repository.Interfaces;
using BlazorApp.Server.Models;

using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.DataBase.Repository
{
    public class SubElementsRepository : ISubElementsRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public SubElementsRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<SubElement?> GetById(int id)
        {
            return await _db.Elements.FindAsync(id);
        }

        public IQueryable<SubElement> GetByQuery()
        {
            return _db.Elements.AsQueryable();
        }

        public async Task<SubElement> CreateAsync(SubElement newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException(nameof(newItem));

            if (_db.Elements.Any(n => n.Id == newItem.Id))
            {
                throw new DuplicateKeyException($"Can't create an object of a type {nameof(SubElement)} with the key '{newItem.Id}'. The object with the same key is already exists");
            }

            await _db.Elements.AddAsync(newItem);
            await _db.SaveChangesAsync();

            return newItem;
        }

        public async Task<SubElement> UpdateAsync(SubElement item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var stored = await _db.Elements.FindAsync(item.Id);
            if (stored == null)
            {
                throw new KeyNotFoundException($"An object of a type '{nameof(SubElement)}' with the key '{item.Id}' not found");
            }

            _mapper.Map(item, stored);
            await _db.SaveChangesAsync();

            return item;
        }

        public void DeleteAsync(int id)
        {
            var item = _db.Elements.Where(n => n.Id == id).ExecuteDelete();
            if (item <= 0)
            {
                throw new KeyNotFoundException($"An object of a type '{nameof(SubElement)}' with the key '{id}' not found");
            }
        }
    }
}
