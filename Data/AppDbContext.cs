using Microsoft.EntityFrameworkCore;
using JobAppTracker.Models;

namespace JobAppTracker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<JobApplication> JobApplications { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
