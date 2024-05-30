using Microsoft.AspNetCore.Authorization;
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
            _reactionService = reactionService ?? throw new ArgumentNullException(nameof(reactionService));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllReactions()
        {
                var reactions = await _reactionService.GetAllReactions();
                return Ok(reactions);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReactionById(int id)
        {
            try
            {
                var reaction = await _reactionService.GetReactionById(id);
                return Ok(reaction);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the reaction with ID {id}: {ex.Message}");
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReaction([FromBody] CreateReactionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdReaction = await _reactionService.CreateReaction(dto);
                return CreatedAtAction(nameof(GetReactionById), new { id = createdReaction.Reaction_Id }, createdReaction);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the reaction: {ex.Message}");
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReaction(int id, [FromBody] UpdateReactionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedReaction = await _reactionService.UpdateReaction(id, dto);
                return Ok(updatedReaction);
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
                return StatusCode(500, $"An error occurred while updating the reaction: {ex.Message}");
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReaction(int id)
        {
            try
            {
                var success = await _reactionService.DeleteReaction(id);
                return NoContent(); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the reaction: {ex.Message}");
            }
        }
    }
}
