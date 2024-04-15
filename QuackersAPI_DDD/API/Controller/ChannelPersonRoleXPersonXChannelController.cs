using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.ChannelPersonRoleXPersonXChannel;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;

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

        [HttpGet("{personId}/{channelId}")]
        public async Task<IActionResult> GetAssociationById(int personId, int channelId)
        {
            try
            {
                var association = await _service.GetAssociationByIds(personId, channelId);
                if (association == null)
                {
                    return NotFound($"No association found for person ID {personId} and channel ID {channelId}.");
                }
                return Ok(association);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssociation([FromBody] CreateChannelPersonRoleXPersonXChannelDTO association)
        {
            try
            {
                var createdAssociation = await _service.CreateAssociation(association);
                return CreatedAtAction(nameof(GetAssociationById), new { personId = createdAssociation.Person_Id, channelId = createdAssociation.Channel_Id }, createdAssociation);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpPut("{personId}/{channelId}")]
        public async Task<IActionResult> UpdateAssociation(int personId, int channelId, [FromBody] UpdateChannelPersonRoleXPersonXChannelDTO dto)
        {
            try
            {
                var updatedAssociation = await _service.UpdateAssociation(personId, channelId, dto);
                return Ok(updatedAssociation);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpDelete("{personId}/{channelId}")]
        public async Task<IActionResult> DeleteAssociation(int personId, int channelId)
        {
            try
            {
                var success = await _service.DeleteAssociation(personId, channelId);
                if (!success)
                {
                    return NotFound($"No association found for person ID {personId} and channel ID {channelId}.");
                }
                return Ok($"Association between person ID {personId} and channel ID {channelId} has been deleted successfully.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }
    }
}

