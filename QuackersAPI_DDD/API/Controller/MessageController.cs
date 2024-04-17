using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.MessageDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessages();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            try
            {
                var message = await _messageService.GetMessageById(id);
                return Ok(message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageDTO messageDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdMessage = await _messageService.CreateMessage(messageDto);
                return CreatedAtAction(nameof(GetMessageById), new { id = createdMessage.Message_Id }, createdMessage);
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
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] UpdateMessageDTO updateMessageDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedMessage = await _messageService.UpdateMessage(id, updateMessageDTO);
                return Ok(updatedMessage);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                bool deleted = await _messageService.DeleteMessage(id);
                if (!deleted)
                {
                    throw new KeyNotFoundException($"Message with id {id} not found.");
                }
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the message: " + ex.Message);
            }
        }
    }
}