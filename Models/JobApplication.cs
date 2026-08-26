namespace JobAppTracker.Models
{
    public enum Status { Applied, Pending, Interview, JobOffer, Rejected }

    public class JobApplication
    {
        public int Id { get; set; }
        public required string CompanyName { get; set; }
        public required string JobTitle { get; set; }
        public required string Location { get; set; }
        public DateTime DateApplied { get; set; } = DateTime.UtcNow;
        public DateTime DateLastUpdate { get; set; } = DateTime.UtcNow;
        public string? URL { get; set; }
        public string? Notes { get; set; }
        public string? Contact { get; set; }
        public Status Status{ get; set; }
    }

}
