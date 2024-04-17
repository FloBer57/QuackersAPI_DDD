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

        [HttpPost]
        public async Task<IActionResult> CreatePersonJobTitle([FromBody] CreatePersonJobTitleDTO createPersonJobTitleDTO)
        {
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonJobTitle()
        {
            var personJobTitles = await _personJobTitleService.GetAllPersonJobTitle();
            return Ok(personJobTitles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonJobTitleById(int id)
        {
            try
            {
                var personJobTitle = await _personJobTitleService.GetPersonJobTitleById(id);
                if (personJobTitle == null)
                    throw new KeyNotFoundException($"Person job title with id {id} not found.");
                return Ok(personJobTitle);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonJobTitle(int id, [FromBody] UpdatePersonJobTitleDTO updatePersonJobTitleDTO)
        {
            try
            {
                var updatedPersonJobTitle = await _personJobTitleService.UpdatePersonJobTitle(id, updatePersonJobTitleDTO);
                return Ok(updatedPersonJobTitle);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonJobTitle(int id)
        {
            try
            {
                var success = await _personJobTitleService.DeletePersonJobTitle(id);
                return Ok($"Person job title with id {id} deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
