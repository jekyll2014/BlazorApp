using BlazorApp.Shared.DTO;

namespace BlazorApp.Server.Services.Interfaces
{
    public interface IProjectService<T, TK>
        where T : IDto
        where TK : IGetQuery
    {
        IEnumerable<T> Get(TK request);
        Task<T> GetById(int id);
        Task<T> CreateAsync(T newItem);
        Task<T> UpdateAsync(T item);
        void DeleteAsync(int id);
    }
}
