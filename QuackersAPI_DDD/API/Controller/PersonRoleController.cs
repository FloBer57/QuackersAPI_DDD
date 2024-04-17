using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonRoleDTO;
using QuackersAPI_DDD.Application.InterfaceService;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonRoleController : ControllerBase
    {
        private readonly IPersonRoleService _personRoleService;

        public PersonRoleController(IPersonRoleService personRoleService)
        {
            _personRoleService = personRoleService ?? throw new ArgumentNullException(nameof(personRoleService));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonRole([FromBody] CreatePersonRoleDTO createPersonRoleDTO)
        {
            try
            {
                var createdPersonRole = await _personRoleService.CreatePersonRole(createPersonRoleDTO);
                return CreatedAtAction(nameof(GetPersonRoleById), new { id = createdPersonRole.PersonRole_Id }, createdPersonRole);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the person role: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonRoles()
        {
                var personRoles = await _personRoleService.GetAllPersonRoles();
                return Ok(personRoles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonRoleById(int id)
        {
            try
            {
                var personRole = await _personRoleService.GetPersonRoleById(id);
                return Ok(personRole);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the person role with ID {id}: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonRole(int id, [FromBody] UpdatePersonRoleDTO updatePersonRoleDTO)
        {
            try
            {
                var updatedPersonRole = await _personRoleService.UpdatePersonRole(id, updatePersonRoleDTO);
                return Ok(updatedPersonRole);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the person role: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonRole(int id)
        {
            try
            {
                var success = await _personRoleService.DeletePersonRole(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the person role: {ex.Message}");
            }
        }
    }
}
    