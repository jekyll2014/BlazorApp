using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Server.Models;

[Serializable]
public class Order : IModelEntity
{
    [Key]
    public int Id { get; set; }
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(100)]
    public string State { get; set; } = string.Empty;
    public List<Window> Windows { get; set; } = new List<Window>();
}