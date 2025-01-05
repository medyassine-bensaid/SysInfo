using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SysInfo.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        [MaxLength(50)]
        public string ClientType { get; set; }

        // Relationships
        [JsonIgnore]

        public ICollection<Project>? Projects { get; set; } = new List<Project>();// Navigation for associated projects
        //public ICollection<Feedback>? Feedbacks { get; set; } // Navigation for feedback
    }
}
