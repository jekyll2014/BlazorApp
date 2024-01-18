using BlazorApp.Server.Services.QueryModels;
using BlazorApp.Shared.DTO;

namespace BlazorApp.Server.Services.Interfaces
{
    public interface IProjectWindowsService : IProjectService<WindowDto, GetWindowQuery>
    {
    }
}
