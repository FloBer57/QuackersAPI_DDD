using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.ChannelDTO;
using QuackersAPI_DDD.Application.Interface;

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllChannels()
        {
                var channels = await _channelService.GetAllChannels();
                return Ok(channels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChannelById(int id)
        {
            try
            {
                var channel = await _channelService.GetChannelById(id);
                return Ok(channel);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the channel with ID {id}: {ex.Message}");
            }
        }

        [HttpGet("{id}/channels")]
        public async Task<IActionResult> GetChannelsByChannelType(int id)
        {
            try
            {
                var channels = await _channelService.GetChannelsByChannelType(id);
                return Ok(channels);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving channels for channel type {id}: {ex.Message}");
            }
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
                return StatusCode(500, $"An internal server error has occurred while creating the channel: {ex.Message}");
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
                return StatusCode(500, $"An internal server error has occurred while updating the channel: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannel(int id)
        {
            try
            {
                await _channelService.DeleteChannel(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the channel: {ex.Message}");
            }
        }

        [HttpGet("{id}/messages")]
        public async Task<IActionResult> GetAllMessagesFromChannel(int id)
        {
            try
            {
                var messages = await _channelService.GetAllMessagesFromChannel(id);
                return Ok(messages);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving messages for the channel with ID {id}: {ex.Message}");
            }
        }
    }
}
