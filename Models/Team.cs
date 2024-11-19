using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysInfo.Models
{

    public class Team
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Team name is required")]
        [MaxLength(100, ErrorMessage = "Team name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Members must be at least 1")]
        public int Members { get; set; }

        [Required(ErrorMessage = "Team leader is required")]
        public User TeamLeader { get; set; } // Navigation property for the leader

        public ICollection<User> TeamMembers { get; set; } // Navigation for team members
    }

}
