using BlazorApp.Server.Models;

namespace BlazorApp.Server.DataBase.Repository.Interfaces
{
    public interface IService<T> where T : IModelEntity
    {
        Task<T?> GetById(int id);
        IQueryable<T> GetByQuery();
        Task<T> CreateAsync(T newItem);
        Task<T> UpdateAsync(T item);
        void DeleteAsync(int id);
    }
}
