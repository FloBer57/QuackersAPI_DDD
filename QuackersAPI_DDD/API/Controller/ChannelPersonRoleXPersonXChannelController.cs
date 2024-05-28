using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.ChannelPersonRoleXPersonXChannel;
using QuackersAPI_DDD.Application.InterfaceService;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelPersonRoleXPersonXChannelController : ControllerBase
    {
        private readonly IChannelPersonRoleXPersonXChannelService _service;

        public ChannelPersonRoleXPersonXChannelController(IChannelPersonRoleXPersonXChannelService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Administrateur")]
        [HttpGet]
        public async Task<IActionResult> GetAllAssociations()
        {
            try
            {
                var associations = await _service.GetAllAssociations();
                return Ok(associations);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [Authorize]
        [HttpGet("{personId}/{channelId}")]
        public async Task<IActionResult> GetAssociationById(int personId, int channelId)
        {
            try
            {
                var association = await _service.GetAssociationByIds(personId, channelId);
                return Ok(association);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [Authorize]
        [HttpGet("channels/{channelId}/roles/{roleId}/persons")]
        public async Task<IActionResult> GetPersonsByRoleInChannel(int channelId, int roleId)
        {
            try
            {
                var persons = await _service.GetPersonsByRoleInChannel(channelId, roleId);
                return Ok(persons);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [Authorize]
        [HttpGet("persons/{personId}/roles")]
        public async Task<IActionResult> GetRolesByPersonInChannels(int personId)
        {
            try
            {
                var roles = await _service.GetRolesByPersonInChannels(personId);
                if (roles == null || !roles.Any())
                {
                    return NotFound("No roles found for this person in any channels.");
                }
                return Ok(roles);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAssociation([FromBody] CreateChannelPersonRoleXPersonXChannelDTO association)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdAssociation = await _service.CreateAssociation(association);
                return CreatedAtAction(nameof(GetAssociationById), new { personId = createdAssociation.Person_Id, channelId = createdAssociation.Channel_Id }, createdAssociation);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [Authorize]
        [HttpPut("{personId}/{channelId}")]
        public async Task<IActionResult> UpdateAssociation(int personId, int channelId, [FromBody] UpdateChannelPersonRoleXPersonXChannelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedAssociation = await _service.UpdateAssociation(personId, channelId, dto);
                return Ok(updatedAssociation);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{personId}/{channelId}")]
        public async Task<IActionResult> DeleteAssociation(int personId, int channelId)
        {
            try
            {
                var success = await _service.DeleteAssociation(personId, channelId);
                return Ok($"Association between person ID {personId} and channel ID {channelId} has been deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }
    }
}
