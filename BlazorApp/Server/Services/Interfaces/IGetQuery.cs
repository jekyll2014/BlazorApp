namespace BlazorApp.Server.Services.Interfaces;

public interface IGetQuery
{
    int? Id { get; set; }
    string? Include { get; set; }
}