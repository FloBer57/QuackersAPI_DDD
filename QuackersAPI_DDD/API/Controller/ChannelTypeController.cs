using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.ChannelTypeDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelTypeController : ControllerBase
    {
        private readonly IChannelTypeService _channelTypeService;

        public ChannelTypeController(IChannelTypeService channelTypeService)
        {
            _channelTypeService = channelTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChannelTypes()
        {
            var channelTypes = await _channelTypeService.GetAllChannelTypes();
            if (channelTypes == null)
            {
                return NotFound("No channel type can be found");
            }
            return Ok(channelTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChannelTypeById(int id)
        {
            var channelType = await _channelTypeService.GetChannelTypeById(id);
            if (channelType == null)
            {
                return NotFound($"ChannelType with id {id} not found.");
            }
            return Ok(channelType);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannelType([FromBody] CreateChannelTypeDTO dto)
        {
            try
            {
                var createdChannelType = await _channelTypeService.CreateChannelType(dto);
                return CreatedAtAction(nameof(GetChannelTypeById), new { id = createdChannelType.ChannelType_Id }, createdChannelType);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChannelType(int id, [FromBody] UpdateChannelTypeDTO dto)
        {
            try { 
            var updatedChannelType = await _channelTypeService.UpdateChannelType(id, dto);
            return Ok(updatedChannelType);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannelType(int id)
        {
            var success = await _channelTypeService.DeleteChannelType(id);
            if (!success)
            {
                return NotFound($"ChannelType with id {id} not found.");
            }
            return Ok($"ChannelType with id {id} deleted successfully.");
        }
    }
}
