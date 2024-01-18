using BlazorApp.Server.Services.Interfaces;

namespace BlazorApp.Server.Services.QueryModels;

public class GetOrderQuery : IGetQuery
{
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? State { get; set; }
    public string? Include { get; set; }
}