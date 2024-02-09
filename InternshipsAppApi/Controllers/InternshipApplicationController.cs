using Application.Services.Interfaces;
using Authentication;
using Domain.Domain;
using Domain.Repository;
using InternshipsAppApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipsAppApi.Controllers
{
    [Route("api/internshipApplications")]
    [ApiController]
    public class InternshipApplicationController : ControllerBase
    {
        private readonly IInternshipApplicationService _internshipApplicationService;

        public InternshipApplicationController(IInternshipApplicationService internshipApplicationService)
        {
            _internshipApplicationService = internshipApplicationService;
        }

        [Route("{internshipid}:{userid}")]
        [HttpGet]
        public async Task<ActionResult<InternshipApplication>> GetInternshipApplicationById(int internshipid, string userid, CancellationToken cancellationToken = default)
        {
            var internshipApplication = await _internshipApplicationService.FindInternshipApplicationById(userid, internshipid, cancellationToken);
            if (internshipApplication == null)
                return NotFound();
            return Ok(internshipApplication);
        }

        [Route("byRegularUserId/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InternshipApplication>>> GetInternshipApplicationsByRegularUserId(string id,
            CancellationToken cancellationToken = default)
        {
            var internshipApplications = await _internshipApplicationService.FindInternshipApplicationssByRegularUserId(id, cancellationToken);
            return Ok(internshipApplications);
        }

        [Route("byInternshipId/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InternshipApplication>>> GetInternshipApplicationsByInternshipId(int id,
            CancellationToken cancellationToken = default)
        {
            var internshipApplications = await _internshipApplicationService.FindInternshipApplicationsByInternshipId(id, cancellationToken);
            return Ok(internshipApplications);
        }

        [HttpPost]
        [Authorize(Roles = Constants.Roles.REGULARUSER)]
        public async Task<ActionResult> CreateReservation([FromBody] InternshipApplicationCreateDTO internshipApplication,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var createdInternshipApplication = await _internshipApplicationService.CreateInternshipApplication(internshipApplication.InternshipId,
                    internshipApplication.RegularUserId, cancellationToken);
                return CreatedAtAction(nameof(GetInternshipApplicationById),
                    new { userid = createdInternshipApplication.RegularUser.Id, internshipid = createdInternshipApplication.Internship.Id },
                    createdInternshipApplication);
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{internshipid}:{userid}")]
        [HttpDelete]
        [Authorize(Roles = Constants.Roles.REGULARUSER)]
        public async Task<ActionResult> DeleteInternshipApplication(int internshipid, string userid, CancellationToken cancellationToken = default)
        {
            try
            {
                await _internshipApplicationService.DeleteInternshipApplication(internshipid, userid, cancellationToken);
                return Ok();
            }
            catch (RepositoryException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
