using System.ComponentModel.DataAnnotations;

namespace SysInfo.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClientType { get; set; }

        // Relationships
        public ICollection<Project> Projects { get; set; } // Navigation for associated projects
        public ICollection<Feedback> Feedbacks { get; set; } // Navigation for feedback
    }
}
