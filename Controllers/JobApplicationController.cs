using JobAppTracker.Data;
using JobAppTracker.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
 
}
