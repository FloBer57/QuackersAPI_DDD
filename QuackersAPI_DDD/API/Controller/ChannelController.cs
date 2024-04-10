using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelService _channelService;

        public ChannelController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChannels()
        {
            var channels = await _channelService.GetAllChannels();
            return Ok(channels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChannelById(int id)
        {
            var channel = await _channelService.GetChannelById(id);
            if (channel == null)
            {
                return NotFound($"Channel with id {id} not found.");
            }
            return Ok(channel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannel([FromBody] Channel channel)
        {
            var createdChannel = await _channelService.CreateChannel(channel);
            return CreatedAtAction(nameof(GetChannelById), new { id = createdChannel.Channel_Id }, createdChannel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChannel(int id, [FromBody] Channel updatedChannel)
        {
            var updated = await _channelService.UpdateChannel(id, updatedChannel);
            if (updated == null)
            {
                return NotFound($"Channel with id {id} not found.");
            }
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannel(int id)
        {
            var success = await _channelService.DeleteChannel(id);
            if (!success)
            {
                return NotFound($"Channel with id {id} not found.");
            }
            return Ok($"Channel with id {id} deleted successfully.");
        }
    }
}
