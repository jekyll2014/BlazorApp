using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Server.Models;

public interface IModelEntity
{
    [Key]
    int Id { get; set; }
}