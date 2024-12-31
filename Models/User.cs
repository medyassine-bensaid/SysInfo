using System.ComponentModel.DataAnnotations;

namespace SysInfo.Models
{
    public class User
    {
        [Key]

        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public int PhoneNumber { get; set; }

        [MaxLength(50)]
        public string Fonction { get; set; }


        [MaxLength(50)]
        public string Profile { get; set; }


        // Relationships
        public virtual ICollection<Team>? Teams { get; set; } = new List<Team>(); // Teams the user is a member of
        public virtual ICollection<Team>? LedTeams { get; set; } = new List<Team>(); // Teams the user leads
    }

}
