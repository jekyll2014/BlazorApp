using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Server.Models
{
    [Serializable]
    public class SubElement : IModelEntity
    {
        [Key]
        public int Id { get; set; }
        public int WindowId { get; set; }
        public int Element { get; set; }
        [MaxLength(100)]
        public string Type { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        public Window Window { get; set; }
    }
}
