using System.ComponentModel.DataAnnotations;

namespace SysInfo.Models
{
    public class Administrator
    {
        [Key]
        public int Id { get; set; } // Use { get; set; } to define a property

        [EmailAddress]
        [Required]
        public string? Email { get; set; } // Optional email validation

        [Required]
        public string? Password { get; set; }
    }
}
