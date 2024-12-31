﻿using System.ComponentModel.DataAnnotations;

namespace SysInfo.Models
{
    public class Administrator
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
