using System.ComponentModel.DataAnnotations;

namespace JobAppTracker.Models
{
    public class CreateJobApplicationDto
    {
        [Required]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        public string JobTitle { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;
        public string? URL { get; set; }
        public string? Notes { get; set; }
        public string? Contact { get; set; }
        public Status Status { get; set; } = Status.Applied;
    }
}
