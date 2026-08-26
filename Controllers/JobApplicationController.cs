using JobAppTracker.Data;
using JobAppTracker.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;

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
            JobApplication? jobApplication = _context.JobApplications.Find(id);

            return jobApplication == null ? NotFound(): Ok(jobApplication);
        }

        [HttpGet("status")]
        public ActionResult<List<JobApplication>> GetJobsByStatus([FromQuery] List<Status> status)
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
                DateApplied = DateTime.UtcNow, 
                DateLastUpdate = DateTime.UtcNow, 
                URL = job.URL, 
                Notes = job.Notes, 
                Contact = job.Contact, 
                Status = job.Status
            };
            _context.JobApplications.Add(newJob);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new { id = newJob.Id }, newJob);
        }

        [HttpPatch("update/{id}")]
        public ActionResult<JobApplication> UpdateJob(int id, JsonPatchDocument<UpdateJobApplicationDto> jobUpdate)
        {
            JobApplication? jobApplication = _context.JobApplications.Find(id);
            if (jobApplication == null) return NotFound();

            UpdateJobApplicationDto updatedJob = new UpdateJobApplicationDto();

            jobUpdate.ApplyTo(updatedJob, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            jobApplication.DateLastUpdate = DateTime.UtcNow;
            jobApplication.URL = updatedJob.URL;
            jobApplication.Notes = updatedJob.Notes;
            jobApplication.Contact = updatedJob.Contact;
            if (updatedJob.Status.HasValue) jobApplication.Status = updatedJob.Status.Value;

            _context.SaveChanges();
            return Ok(jobApplication);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteJob(int id)
        {
            JobApplication? jobApplication = _context.JobApplications.Find(id);
            if (jobApplication == null) return NotFound();
            _context.Remove(jobApplication);
            _context.SaveChanges();
            return NoContent();
        }
    }
 
}
