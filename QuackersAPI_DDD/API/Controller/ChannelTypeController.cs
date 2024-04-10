using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateChannelType([FromBody] ChannelType channelType)
        {
            var createdChannelType = await _channelTypeService.CreateChannelType(channelType);
            return CreatedAtAction(nameof(GetChannelTypeById), new { id = createdChannelType.ChannelType_Id }, createdChannelType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChannelTypeName(int id, [FromBody] ChannelType channelType)
        {
            var existingChannelType = await _channelTypeService.GetChannelTypeById(id);
            if (existingChannelType == null)
            {
                return NotFound($"ChannelType with id {id} not found.");
            }

            existingChannelType.ChannelType_Name = channelType.ChannelType_Name;

            var updatedChannelType = await _channelTypeService.UpdateChannelType(id, existingChannelType);
            return Ok(updatedChannelType);
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
