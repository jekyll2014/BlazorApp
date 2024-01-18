using BlazorApp.Server.Services.QueryModels;
using BlazorApp.Shared.DTO;

namespace BlazorApp.Server.Services.Interfaces
{
    public interface IProjectSubElementsService : IProjectService<SubElementDto, GetSubElementQuery>
    {
    }
}
