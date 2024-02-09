using Application.Services.Interfaces;
using Domain.Domain;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InternshipsAppApi.Controllers
{
    [Route("api/regularUsers")]
    [ApiController]
    public class RegularUserController : ControllerBase
    {
        private readonly IRegularUserService _regularUserService;

        public RegularUserController(IRegularUserService regularUserService)
        {
            _regularUserService = regularUserService;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<RegularUser>> GetRegularUserById(string id, CancellationToken cancellationToken = default)
        {
            var regularUserDto = await _regularUserService.FindRegularUserById(id, cancellationToken);
            if (regularUserDto == null)
                return NotFound();
            return Ok(regularUserDto);
        }

        [Route("byname/{username}")]
        [HttpPost]
        public async Task<ActionResult<RegularUser>> GetRegularUserByUsername(string username,
            CancellationToken cancellationToken = default)
        {
            var regularUserDto = await _regularUserService.FindRegularUserByUsername(username, cancellationToken);
            if (regularUserDto == null)
                return NotFound();
            return Ok(regularUserDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRegularUser([FromBody] RegularUser regularUser, CancellationToken cancellationToken = default)
        {
            try
            {
                var createdRegularUser = await _regularUserService.CreateRegularUser(regularUser.Id, regularUser.Username,
                    cancellationToken);
                return CreatedAtAction(nameof(GetRegularUserById), new { id = createdRegularUser.Id }, createdRegularUser);
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
