using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.ReactionDTO;
using QuackersAPI_DDD.Application.InterfaceService;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionService _reactionService;

        public ReactionController(IReactionService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReactions()
        {
            var reactions = await _reactionService.GetAllReactions();
            return Ok(reactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReactionById(int id)
        {
            var reaction = await _reactionService.GetReactionById(id);
            if (reaction == null)
            {
                return NotFound($"Reaction with id {id} not found.");
            }
            return Ok(reaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReaction([FromBody] CreateReactionDTO dto)
        {
            var createdReaction = await _reactionService.CreateReaction(dto);
            return CreatedAtAction(nameof(GetReactionById), new { id = createdReaction.Reaction_Id }, createdReaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReaction(int id, [FromBody] UpdateReactionDTO dto)
        {
            var updatedReaction = await _reactionService.UpdateReaction(id, dto);
            if (updatedReaction == null)
            {
                return NotFound($"Reaction with id {id} not found.");
            }
            return Ok(updatedReaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReaction(int id)
        {
            var success = await _reactionService.DeleteReaction(id);
            if (!success)
            {
                return NotFound($"Reaction with id {id} not found.");
            }
            return Ok($"Reaction with id {id} deleted successfully.");
        }
    }
}
