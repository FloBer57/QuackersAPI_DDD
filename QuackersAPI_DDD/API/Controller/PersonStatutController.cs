using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonStatutDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonStatutController : ControllerBase
    {
        private readonly IPersonStatutService _personStatutService;

        public PersonStatutController(IPersonStatutService personStatutService)
        {
            _personStatutService = personStatutService ?? throw new ArgumentNullException(nameof(personStatutService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonStatut>>> GetAllPersonStatuts()
        {
            var personStatuts = await _personStatutService.GetAllPersonStatuts();
            if (personStatuts == null)
            {
                return NotFound("No PersonStatus can be found");
            }
            return Ok(personStatuts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonStatut>> GetPersonStatutById(int id)
        {
            var personStatut = await _personStatutService.GetPersonStatutById(id);
            if (personStatut == null)
            {
                return NotFound();
            }
            return Ok(personStatut);
        }

        [HttpPost]
        public async Task<ActionResult<PersonStatut>> CreatePersonStatut([FromBody] CreatePersonStatutDTO personStatut)
        {
            var createdPersonStatut = await _personStatutService.CreatePersonStatut(personStatut);
            return CreatedAtAction(nameof(GetPersonStatutById), new { id = createdPersonStatut.PersonStatut_Id }, createdPersonStatut);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonStatut(int id, [FromBody] UpdatePersonStatutDTO personStatut)
        {

            var updatedPersonStatut = await _personStatutService.UpdatePersonStatut(id, personStatut);
            if (updatedPersonStatut == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonStatut(int id)
        {
            var result = await _personStatutService.DeletePersonStatut(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
