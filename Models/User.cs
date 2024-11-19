using System.ComponentModel.DataAnnotations;

namespace SysInfo.Models
{
    public class User
    {
        [Key]

        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Fonction { get; set; }


        [Required]
        [MaxLength(50)]
        public string Profile { get; set; }


        // Relationships
        public ICollection<Team> Teams { get; set; } // Teams the user is a member of
        public ICollection<Team> LedTeams { get; set; } // Teams the user leads
    }

}
