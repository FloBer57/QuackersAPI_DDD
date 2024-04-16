using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.ChannelDTO;
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
            if (channels == null)
            {
                return NotFound("No channel can be found");
            }
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

        [HttpGet("{id}/channels")]
        public async Task<IActionResult> GetChannelsByChannelType(int id)
        {
            var channels = await _channelService.GetChannelsByChannelType(id);
            if (channels == null)
            {
                return NotFound($"No channel with ChannelTypeId = {id} can be found");
            }
            return Ok(channels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannel([FromBody] CreateChannelDTO createChannelDTO)
        {
            try
            {
                var createdChannel = await _channelService.CreateChannel(createChannelDTO);
                return CreatedAtAction(nameof(GetChannelById), new { id = createdChannel.Channel_Id }, createdChannel);
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
        public async Task<IActionResult> UpdateChannel(int id, [FromBody] UpdateChannelDTO updateChannelDTO)
        {
            try
            {
                var updatedChannel = await _channelService.UpdateChannel(id, updateChannelDTO);
                return Ok(updatedChannel);
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
        public async Task<IActionResult> DeleteChannel(int id)
        {
            try
            {
                var success = await _channelService.DeleteChannel(id);
                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Channel: {ex.Message}");
            }
        }
    }
}
