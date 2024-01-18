using AutoMapper;

using BlazorApp.Exceptions;
using BlazorApp.Server.DataBase.Repository.Interfaces;
using BlazorApp.Server.Models;

using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Server.DataBase.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public OrdersRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Order?> GetById(int id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public IQueryable<Order> GetByQuery()
        {
            return _db.Orders.AsQueryable();
        }

        public async Task<Order> CreateAsync(Order newItem)
        {
            if (newItem == null)
                throw new ArgumentNullException(nameof(newItem));

            if (_db.Orders.Any(n => n.Id == newItem.Id))
            {
                throw new DuplicateKeyException($"Can't create an object of a type {nameof(Order)} with the key '{newItem.Id}'. The object with the same key is already exists");
            }

            await _db.Orders.AddAsync(newItem);
            await _db.SaveChangesAsync();

            return newItem;
        }

        public async Task<Order> UpdateAsync(Order item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var stored = await _db.Orders.FindAsync(item.Id);
            if (stored == null)
            {
                throw new KeyNotFoundException($"An object of a type '{nameof(Order)}' with the key '{item.Id}' not found");
            }

            _mapper.Map(item, stored);
            await _db.SaveChangesAsync();

            return item;
        }

        public void DeleteAsync(int id)
        {
            var item = _db.Orders.Where(n => n.Id == id).ExecuteDelete();
            if (item <= 0)
            {
                throw new KeyNotFoundException($"An object of a type '{nameof(Order)}' with the key '{id}' not found");
            }
        }
    }
}
