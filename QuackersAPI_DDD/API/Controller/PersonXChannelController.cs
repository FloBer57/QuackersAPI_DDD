using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonXChannelDTO;
using QuackersAPI_DDD.Application.InterfaceService;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var associations = await _service.GetAllAssociations();
            return Ok(associations);
        }

        [HttpGet("{personId}/{channelId}")]
        public async Task<IActionResult> GetById(int personId, int channelId)
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonXChannelDTO dto)
        {
            try
            {
                var createdAssociation = await _service.CreateAssociation(dto);
                return CreatedAtAction(nameof(GetById), new { personId = dto.PersonId, channelId = dto.ChannelId }, createdAssociation);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpPost("addPersonToChannel/{personId}/{channelId}")]  // Corrected route
        public async Task<IActionResult> AddPersonToChannel(int personId, int channelId)
        {
            try
            {
                await _service.AddPersonToChannel(personId, channelId);  
                return CreatedAtAction(nameof(GetById), new { personId, channelId }, null);  
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpPut("{personId}/{channelId}")]
        public async Task<IActionResult> Update(int personId, int channelId, [FromBody] UpdatePersonXChannelDTO dto)
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
        public async Task<IActionResult> Delete(int personId, int channelId)
        {
            try
            {
                var success = await _service.DeleteAssociation(personId, channelId);
                if (!success)
                {
                    return NotFound($"Association not found with person ID {personId} and channel ID {channelId}.");
                }
                return Ok($"Association successfully deleted.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }

        [HttpPost("addPersonToChannel")]
        public async Task<IActionResult> AddPersonToChannel([FromBody] CreatePersonXChannelDTO dto)
        {
            try
            {
                await _service.AddPersonToChannel(dto.PersonId, dto.ChannelId);
                return CreatedAtAction(nameof(GetById), new { personId = dto.PersonId, channelId = dto.ChannelId }, null);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }
    }
}
