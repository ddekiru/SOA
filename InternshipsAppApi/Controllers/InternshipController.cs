using Application.Services.Interfaces;
using Authentication;
using Domain.Domain;
using Domain.Repository;
using InternshipsAppApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipsAppApi.Controllers
{
    [Route("api/internships")]
    [ApiController]
    public class InternshipController : ControllerBase
    {
        private readonly IInternshipService _internshipService;

        public InternshipController(IInternshipService internshipService)
        {
            _internshipService = internshipService;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Internship>> GetInternshipById(int id, CancellationToken cancellationToken = default)
        {
            var internship = await _internshipService.FindInternshipById(id, cancellationToken);
            if (internship == null)
                return NotFound();
            return Ok(internship);
        }

        [Route("{internshipid}")]
        [HttpDelete]
        [Authorize(Roles=Constants.Roles.ADMINUSER)]
        public async Task<ActionResult> DeleteInternship(int internshipid, CancellationToken cancellationToken = default)
        {
            try
            {
                await _internshipService.DeleteInternship(internshipid, cancellationToken);
                return Ok();
            }
            catch (RepositoryException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Internship>>> GetInternships(
            [FromQuery(Name = "title")] string? title,
            [FromQuery(Name = "location")] string? location,
            [FromQuery(Name = "domain")] string? domain,
            [FromQuery(Name = "companyName")] string? companyName,
            [FromQuery(Name = "date")] string? dateString,
            CancellationToken cancellationToken = default)
        {
            DateTime? date = null;
            if (title == null)
                title = "";
            if (location == null)
                location = "";
            if (domain == null)
                domain = "";
            if (companyName == null)
                companyName = "";
            if (dateString != null)
                date = DateTime.Parse(dateString);
            var filteredTrips = await _internshipService
                .FindInternshipsFiltered(title, location, domain, companyName, date, cancellationToken);
            return Ok(filteredTrips);
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.ADMINUSER)]
        public async Task<ActionResult> CreateInternship([FromBody] InternshipCreateDTO internship, CancellationToken cancellationToken = default)
        {
            try
            {
                var createdTrip = await _internshipService.CreateInternship(internship.AdminUserID, internship.Title, internship.Location,
                    internship.Domain, internship.Description,
                    DateTime.Now,
                    DateTime.Parse(internship.Deadline), cancellationToken);
                return CreatedAtAction(nameof(GetInternshipById), new { id = createdTrip.Id }, createdTrip);
            }
            catch (Exception ex) when (ex is RepositoryException || ex is ArgumentException)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
