using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Shared.DTO
{
    [Serializable]
    public class SubElementDto : IDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int WindowId { get; set; }
        public int Element { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
