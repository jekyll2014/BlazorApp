using AutoMapper;

using BlazorApp.Exceptions;
using BlazorApp.Server.DataBase.Repository.Interfaces;
using BlazorApp.Server.Models;

using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.DataBase.Repository
{
    public class WindowsRepository : IWindowsRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public WindowsRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Window?> GetById(int id)
        {
            return await _db.Windows.FindAsync(id);
        }

        public IQueryable<Window> GetByQuery()
        {
            return _db.Windows.AsQueryable();
        }

        public async Task<Window> CreateAsync(Window newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException(nameof(newItem));

            if (_db.Windows.Any(n => n.Id == newItem.Id))
            {
                throw new DuplicateKeyException($"Can't create an object of a type {nameof(Window)} with the key '{newItem.Id}'. The object with the same key is already exists");
            }

            await _db.Windows.AddAsync(newItem);
            await _db.SaveChangesAsync();

            return newItem;
        }

        public async Task<Window> UpdateAsync(Window item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var stored = await _db.Windows.FindAsync(item.Id);
            if (stored == null)
            {
                throw new KeyNotFoundException($"An object of a type '{nameof(Window)}' with the key '{item.Id}' not found");
            }

            _mapper.Map(item, stored);
            await _db.SaveChangesAsync();

            return item;
        }

        public void DeleteAsync(int id)
        {
            var item = _db.Windows.Where(n => n.Id == id).ExecuteDelete();
            if (item <= 0)
            {
                throw new KeyNotFoundException($"An object of a type '{nameof(Window)}' with the key '{id}' not found");
            }
        }
    }
}
