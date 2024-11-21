   using System;
    using System.ComponentModel.DataAnnotations;

namespace SysInfo.Models
{
 

    public class Project
    {
        [Key]
        public int Id { get; set; }

        //[Required(ErrorMessage = "Project name is required")]
        //[MaxLength(100, ErrorMessage = "Project name cannot exceed 100 characters")]
        public string Name { get; set; }

       // [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

       // [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        // Foreign Keys
     //   [Required]
        public int TeamId { get; set; }
        public Team Team { get; set; }

       // [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }

}
