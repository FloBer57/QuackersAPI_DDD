using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonStatutDTO;
using QuackersAPI_DDD.Application.InterfaceService;

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

        [HttpPost]
        public async Task<IActionResult> CreatePersonStatut([FromBody] CreatePersonStatutDTO personStatutDTO)
        {
            try
            {
                var createdPersonStatut = await _personStatutService.CreatePersonStatut(personStatutDTO);
                return CreatedAtAction(nameof(GetPersonStatutById), new { id = createdPersonStatut.PersonStatut_Id }, createdPersonStatut);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the person status: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonStatuts()
        {
                var personStatuts = await _personStatutService.GetAllPersonStatuts();
                return Ok(personStatuts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonStatutById(int id)
        {
            try
            {
                var personStatut = await _personStatutService.GetPersonStatutById(id);
                return Ok(personStatut);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the person status with ID {id}: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonStatut(int id, [FromBody] UpdatePersonStatutDTO personStatutDTO)
        {
            try
            {
                var updatedPersonStatut = await _personStatutService.UpdatePersonStatut(id, personStatutDTO);
                return Ok(updatedPersonStatut);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);  // Assuming the message explains the conflict
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the person status: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonStatut(int id)
        {
            try
            {
                var success = await _personStatutService.DeletePersonStatut(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the person status: {ex.Message}");
            }
        }
    }
}
