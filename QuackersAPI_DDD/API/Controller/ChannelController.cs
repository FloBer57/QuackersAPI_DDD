using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Request;
using QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Response;
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

        [HttpPost]
        public async Task<IActionResult> CreateChannel([FromBody] CreateChannelRequestDTO createChannelRequestDTO)
        {
            CreateChannelResponseDTO createResponse = await _channelService.CreateChannel(createChannelRequestDTO);
            if (createResponse == null)
            {
                return BadRequest("Une erreur est arrivé, impossible de crée un utilisateur.");
            }
            return CreatedAtAction(nameof(GetChannelById), new { id = createResponse.Id }, createResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllChannel()
        {
            var channels = await _channelService.GetAllChannel();
            return Ok(channels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChannelById(int id)
        {
            var channel = await _channelService.GetChannelById(id);
            if (channel == null)
            {
                return NotFound($"Le channel {id} n'as pas été trouvé..");
            }
            return Ok(channel);
        }

        [HttpPatch("update-channel-name/{id}")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateChannelNameRequestDTO request)
        {
            var updateResponse = await _channelService.UpdateName(id, request.NewName);
            if (!updateResponse.Success)
            {
                return BadRequest(updateResponse.Message);
            }
            return Ok(updateResponse.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChannel(int id)
        {
            var deleteResponse = await _channelService.DeleteChannel(id);
            if (!deleteResponse.Success)
            {
                return NotFound(deleteResponse.Message);
            }
            return Ok(deleteResponse.Message);
        }
    }
}
