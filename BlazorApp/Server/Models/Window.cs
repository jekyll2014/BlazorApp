using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Server.Models;

[Serializable]
public class Window : IModelEntity
{
    [Key]
    public int Id { get; set; }
    public int OrderId { get; set; }
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    public int QuantityOfWindows { get; set; }
    public List<SubElement> Elements { get; set; } = new List<SubElement>();
    public Order Order { get; set; }
}