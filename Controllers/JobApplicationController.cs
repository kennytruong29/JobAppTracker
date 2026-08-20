using JobAppTracker.Data;
using JobAppTracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;

namespace JobAppTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobApplicationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobApplicationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<JobApplication>> GetAllJobs()
        {
            return Ok(_context.JobApplications.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<JobApplication> GetByID(int id)
        {
            JobApplication? JobApplication = _context.JobApplications.Find(id);

            return JobApplication == null ? NotFound(): Ok(JobApplication);
        }

        [HttpGet("status")]
        public ActionResult<List<JobApplication>> GetJobsByStatus(List<Status> status)
        {
            return Ok(_context.JobApplications.Where(job => status.Contains(job.Status)).ToList());
        }

        [HttpGet("daterange")]
        public ActionResult<List<JobApplication>> GetJobsByDateRange(DateTime startingDate, DateTime endingDate)
        {
            return Ok(_context.JobApplications.Where(job => job.DateApplied >= startingDate && job.DateApplied <= endingDate).ToList());
        }

        [HttpPost("addjob")]
        public ActionResult<JobApplication> AddJob([FromBody] CreateJobApplicationDto job)
        {
            JobApplication newJob = new JobApplication { 
                CompanyName = job.CompanyName, 
                JobTitle = job.JobTitle, 
                Location = job.Location, 
                DateApplied = DateTime.Now, 
                DateLastUpdate = DateTime.Now, 
                URL = job.URL, 
                Notes = job.Notes, 
                Contact = job.Contact, 
                Status = job.Status
            };
            _context.JobApplications.Add(newJob);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { id = newJob.Id }, newJob);
        }

    }
 
}
