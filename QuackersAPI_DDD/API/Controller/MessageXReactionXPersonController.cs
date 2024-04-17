using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.MessagexreactionxpersonDTO;
using QuackersAPI_DDD.Application.InterfaceService;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagexreactionxpersonController : ControllerBase
    {
        private readonly IMessageXReactionXPersonService _service;

        public MessagexreactionxpersonController(IMessageXReactionXPersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reactions = await _service.GetAllReactions();
            return Ok(reactions);
        }

        [HttpGet("{personId}/{messageId}/{reactionId}")]
        public async Task<IActionResult> GetById(int personId, int messageId, int reactionId)
        {
            try
            {
                var reaction = await _service.GetReactionById(personId, messageId, reactionId);
                if (reaction == null)
                {
                    throw new KeyNotFoundException($"Reaction not found with person ID {personId}, message ID {messageId}, and reaction ID {reactionId}.");
                }
                return Ok(reaction);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMessageXReactionXPersonDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdReaction = await _service.CreateReaction(dto);
                return CreatedAtAction(nameof(GetById), new { personId = dto.PersonId, messageId = dto.MessageId, reactionId = dto.ReactionId }, createdReaction);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{personId}/{messageId}/{reactionId}")]
        public async Task<IActionResult> Update(int personId, int messageId, int reactionId, [FromBody] UpdateMessageXReactionXPersonDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedReaction = await _service.UpdateReaction(personId, messageId, reactionId, dto);
                return Ok(updatedReaction);
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

        [HttpDelete("{personId}/{messageId}/{reactionId}")]
        public async Task<IActionResult> Delete(int personId, int messageId, int reactionId)
        {
            try
            {
                bool deleted = await _service.DeleteReaction(personId, messageId, reactionId);
                if (!deleted)
                {
                    throw new KeyNotFoundException($"Reaction not found with person ID {personId}, message ID {messageId}, and reaction ID {reactionId}.");
                }
                return NoContent();
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
    }
}
