namespace SysInfo.Models
{

    public enum SatisfactionLevel
    {
        NonSatisfait,
        ModerementSatisfait,
        Satisfait
    }
    public class Feedback
    {
        public int Id { get; set; }
        public int ResponseTime { get; set; }
        public int SystemPerformanceMetrics { get; set; }
        public int CustomerSupportQuality { get; set; }

        public SatisfactionLevel SatisfactionLevel { get; set; }

        // Relationships
        public int ClientId { get; set; } // Foreign key for Client
        public Client Client { get; set; } // Navigation property for Client

        public int ProjectId { get; set; } // Foreign key for Project
        public Project Project { get; set; } // Navigation property for Project
    }

}
