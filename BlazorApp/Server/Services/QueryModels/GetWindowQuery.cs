using BlazorApp.Server.Services.Interfaces;

namespace BlazorApp.Server.Services.QueryModels;

public class GetWindowQuery : IGetQuery
{
    public int? Id { get; set; }
    public int? OrderId { get; set; }
    public string? Name { get; set; }
    public int? QuantityOfWindows { get; set; }
    public string? Include { get; set; }
}