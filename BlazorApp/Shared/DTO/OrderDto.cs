using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared.DTO;

[Serializable]
public class OrderDto : IDto
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public List<WindowDto>? Windows { get; set; } = new List<WindowDto>();
}