  using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SysInfo.Models
{

    public class Team
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Team name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Members must be at least 1")]
        public int Members { get; set; }


        [ForeignKey("User")]
        public int? TeamLeaderId { get; set; }
        public virtual User? TeamLeader { get; set; } // Navigation property for the leader

        public virtual ICollection<User> TeamMembers { get; set; } = new List<User>(); // Navigation for team members
        [JsonIgnore]
        public ICollection<Project> Projects { get; set; } = new List<Project>();

    }

}
