   using System;
    using System.ComponentModel.DataAnnotations;

namespace SysInfo.Models
{
 

    public class Project
    {
        [Key]
        [Required]
        public int? Id { get; set; }

        [MaxLength(100, ErrorMessage = "Project name cannot exceed 100 characters")]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        // Foreign Keys
        public ICollection<Team>? Teams { get; set; } = new List<Team>();

        public ICollection<Client>? Clients { get; set; } = new List<Client>();

    }

}
