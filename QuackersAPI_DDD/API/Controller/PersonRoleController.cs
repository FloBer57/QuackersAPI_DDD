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
            var createdPersonRole = await _personRoleService.CreatePersonRole(createPersonRoleDTO);
            return CreatedAtAction(nameof(GetPersonRoleById), new { id = createdPersonRole.PersonRole_Id }, createdPersonRole);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonRoles()
        {
            var personRoles = await _personRoleService.GetAllPersonRoles();
            if (personRoles == null)
            {
                return NotFound("No PersonRole can be found");
            }
            return Ok(personRoles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonRoleById(int id)
        {
            var personRole = await _personRoleService.GetPersonRoleById(id);
            if (personRole == null)
            {
                return NotFound("No PersonRole with id = {id} can be found");
            }
            return Ok(personRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersonRole(int id, [FromBody] UpdatePersonRoleDTO updatePersonRoleDTO)
        {
            var updatedPersonRole = await _personRoleService.UpdatePersonRole(id, updatePersonRoleDTO);
            return Ok(updatedPersonRole);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonRole(int id)
        {
            var result = await _personRoleService.DeletePersonRole(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
    