using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared.DTO;

public interface IDto
{
    [Key]
    public int Id { get; set; }
}