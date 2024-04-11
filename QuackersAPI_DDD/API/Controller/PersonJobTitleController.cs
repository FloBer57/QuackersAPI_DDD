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
            var createdPersonJobTitle = await _personJobTitleService.CreatePersonJobTitle(createPersonJobTitleDTO);
            return CreatedAtAction(nameof(GetPersonJobTitleById), new { id = createdPersonJobTitle.PersonJob_TitleId }, createdPersonJobTitle);
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
            var personJobTitle = await _personJobTitleService.GetPersonJobTitleById(id);
            if (personJobTitle == null)
            {
                return NotFound($"Person job title with id {id} not found.");
            }
            return Ok(personJobTitle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonJobTitle(int id, [FromBody] UpdatePersonJobTitleDTO updatePersonJobTitleDTO)
        {
            var updatedPersonJobTitle = await _personJobTitleService.UpdatePersonJobTitle(id, updatePersonJobTitleDTO);
            if (updatedPersonJobTitle == null)
            {
                return NotFound($"Person job title with id {id} not found.");
            }
            return Ok(updatedPersonJobTitle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonJobTitle(int id)
        {
            var success = await _personJobTitleService.DeletePersonJobTitle(id);
            if (!success)
            {
                return NotFound($"Person job title with id {id} not found.");
            }
            return Ok($"Person job title with id {id} deleted successfully.");
        }
    }
}
