using Application.Services.Interfaces;
using Domain.Domain;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InternshipsAppApi.Controllers
{
    [Route("api/adminUsers")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<AdminUser>> GetAgencyUserById(string id, CancellationToken cancellationToken = default)
        {
            var agencyUserDto = await _adminUserService.FindAdminUserById(id, cancellationToken);
            if (agencyUserDto == null)
                return NotFound();
            return Ok(agencyUserDto);
        }

        [Route("byname/{username}")]
        [HttpPost]
        public async Task<ActionResult<AdminUser>> GetAgencyUserByUsername(string username,
            CancellationToken cancellationToken = default)
        {
            var agencyUserDto = await _adminUserService.FindAdminUserByUsername(
                username, cancellationToken);
            if (agencyUserDto == null)
                return NotFound();
            return Ok(agencyUserDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAgencyUser([FromBody] AdminUser adminUser, CancellationToken cancellationToken = default)
        {
            try
            {
                var createdAgencyUser = await _adminUserService.CreateAdminUser(adminUser.Id, adminUser.Username,
                    adminUser.CompanyName, cancellationToken);
                return CreatedAtAction(nameof(GetAgencyUserById), new { id = createdAgencyUser.Id }, createdAgencyUser);
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
