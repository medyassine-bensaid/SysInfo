namespace SysInfo.ViewModel
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // IDs for selected teams and clients
        public List<int> TeamIds { get; set; } = new List<int>();
        public List<int> ClientIds { get; set; } = new List<int>();
    }
}
