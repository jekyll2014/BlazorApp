using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Server.Models
{
    public class User
    {
        public List<string> Roles { get; set; } = new List<string>();
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; } = "";
    }
}
