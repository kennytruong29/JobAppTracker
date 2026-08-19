namespace JobAppTracker.Models
{
    public enum Status { Applied, Pending, Interview, JobOffer, Rejected }

    public class JobApplication
    {
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public int Id { get; set; }
        public DateTime DateApplied { get; set; } = DateTime.Now;
        public DateTime DateLastUpdate { get; set; } = DateTime.Now;
        public string? URL { get; set; }
        public string? Notes { get; set; }
        public string? Contact { get; set; }
        public Status Status{ get; set; }
    }

}
