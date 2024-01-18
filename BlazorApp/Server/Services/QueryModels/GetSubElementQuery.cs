using BlazorApp.Server.Services.Interfaces;

namespace BlazorApp.Server.Services.QueryModels;

public class GetSubElementQuery : IGetQuery
{
    public int? Id { get; set; }
    public int? WindowId { get; set; }
    public int? Element { get; set; }
    public string? Type { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? Include { get; set; }
}