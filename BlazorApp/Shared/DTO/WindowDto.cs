using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared.DTO;

[Serializable]
public class WindowDto : IDto
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int OrderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int QuantityOfWindows { get; set; }
    public List<SubElementDto>? Elements { get; set; } = null;
    public int TotalSubElements => Elements?.Count ?? -1;
}