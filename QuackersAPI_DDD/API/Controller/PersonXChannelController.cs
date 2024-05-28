using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonXChannelDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Application.Service;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonXchannelController : ControllerBase
    {
        private readonly IPersonXChannelService _service;

        public PersonXchannelController(IPersonXChannelService service)
        {
            _service = service;
        }

        [Authorize(Roles ="Administrateur")]
        [HttpGet]
        public async Task<IActionResult> GetAllAssociations()
        {
            var associations = await _service.GetAllAssociations();
            return Ok(associations);
        }
        [Authorize]
        [HttpGet("{personId}/{channelId}")]
        public async Task<IActionResult> GetAssociationById(int personId, int channelId)
        {
            try
            {
                var association = await _service.GetAssociationById(personId, channelId);
                return Ok(association);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAssociation([FromBody] CreatePersonXChannelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdAssociation = await _service.CreateAssociation(dto);
                return CreatedAtAction(nameof(GetAssociationById), new { personId = dto.PersonId, channelId = dto.ChannelId }, createdAssociation);
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
        [Authorize]
        [HttpPut("{personId}/{channelId}")]
        public async Task<IActionResult> UpdateAssociation(int personId, int channelId, [FromBody] UpdatePersonXChannelDTO dto)
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
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }
        [Authorize]
        [HttpDelete("{personId}/{channelId}")]
        public async Task<IActionResult> Delete(int personId, int channelId)
        {
            try
            {
                var success = await _service.DeleteAssociation(personId, channelId);
                return Ok($"Association successfully deleted.");
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
        [Authorize]
        [HttpGet("channels/{channelId}/persons")]
        public async Task<IActionResult> GetPersonsByChannelId(int channelId)
        {
            try
            {
                var persons = await _service.GetPersonsByChannelId(channelId);
                if (!persons.Any())
                {
                    return NotFound($"No persons found for channel with ID {channelId}.");
                }
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred: " + ex.Message);
            }
        }
        [Authorize]
        [HttpGet("persons/{personId}/channels")]
        public async Task<IActionResult> GetChannelsByPersonId(int personId)
        {
            try
            {
                var channels = await _service.GetChannelsByPersonId(personId);
                if (channels == null || !channels.Any())
                {
                    return NotFound($"No channels found for person with ID {personId}.");
                }
                return Ok(channels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred: " + ex.Message);
            }
        }

    }
   
}
