namespace JobAppTracker.Models
{
    public class UpdateJobApplicationDto
    {
        public DateTime DateLastUpdate { get; set; }
        public string? URL { get; set; }
        public string? Notes { get; set; }
        public string? Contact { get; set;}
        public Status? Status { get; set; }
    }
}
