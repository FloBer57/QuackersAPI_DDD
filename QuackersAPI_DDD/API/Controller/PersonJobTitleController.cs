using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonJobTitleDTO;
using QuackersAPI_DDD.Application.InterfaceService;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonJobTitleController : ControllerBase
    {
        private readonly IPersonJobTitleService _personJobTitleService;

        public PersonJobTitleController(IPersonJobTitleService personJobTitleService)
        {
            _personJobTitleService = personJobTitleService ?? throw new ArgumentNullException(nameof(personJobTitleService));
        }

        [Authorize(Roles ="Administrateur")]
        [HttpPost]
        public async Task<IActionResult> CreatePersonJobTitle([FromBody] CreatePersonJobTitleDTO createPersonJobTitleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdPersonJobTitle = await _personJobTitleService.CreatePersonJobTitle(createPersonJobTitleDTO);
                return CreatedAtAction(nameof(GetPersonJobTitleById), new { id = createdPersonJobTitle.PersonJob_TitleId }, createdPersonJobTitle);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An internal server error has occurred: {ex.Message}");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPersonJobTitle()
        {

                var personJobTitles = await _personJobTitleService.GetAllPersonJobTitle();
                return Ok(personJobTitles);

        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonJobTitleById(int id)
        {
            try
            {
                var personJobTitle = await _personJobTitleService.GetPersonJobTitleById(id);
                return Ok(personJobTitle);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the person job title with ID {id}: {ex.Message}");
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonJobTitle(int id, [FromBody] UpdatePersonJobTitleDTO updatePersonJobTitleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedPersonJobTitle = await _personJobTitleService.UpdatePersonJobTitle(id, updatePersonJobTitleDTO);
                return Ok(updatedPersonJobTitle);
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
                return StatusCode(500, $"An internal server error has occurred while updating the person job title: {ex.Message}");
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonJobTitle(int id)
        {
            try
            {
                var success = await _personJobTitleService.DeletePersonJobTitle(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the person job title: {ex.Message}");
            }
        }
    }
}
