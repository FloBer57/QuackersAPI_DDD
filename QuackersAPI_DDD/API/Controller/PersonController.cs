using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.PersonDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ISecurityService _securityService;

        public PersonController(IPersonService personService, ISecurityService securityService)
        {
            _personService = personService;
            _securityService = securityService;
        }
        [Authorize(Roles ="Administrateur")]
        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            try
            {
                var persons = await _personService.GetAllPersons();
                return Ok(persons);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            try
            {
                var person = await _personService.GetPersonById(id);
                return person != null ? Ok(person) : NotFound($"No person found with ID {id}.");
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonDTO createPersonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var person = await _personService.CreatePerson(createPersonDTO);
                return CreatedAtAction(nameof(GetPersonById), new { id = person.Person_Id }, person);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
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
        [Authorize]
        [HttpPost("test")]
        public async Task<IActionResult> CreatePersonTest([FromBody] CreatePersonTestDTO createPersonTestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var person = await _personService.CreatePersonTest(createPersonTestDTO);
                return CreatedAtAction(nameof(GetPersonById), new { id = person.Person_Id }, person);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error: " + e.Message);
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] UpdatePersonDTO updatePersonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedPerson = await _personService.UpdatePerson(id, updatePersonDTO);
                return Ok(updatedPerson);
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
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var result = await _personService.DeletePerson(id);
                if (!result)
                {
                    return NotFound($"No person found with ID {id}.");
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
        [Authorize]
        [HttpGet("ByJobTitle/{jobTitleId}")]
        public async Task<IActionResult> GetPersonsByJobTitle(int jobTitleId)
        {
            try
            {
                var persons = await _personService.GetPersonsByJobTitle(jobTitleId);
                return Ok(persons);
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
        [Authorize]
        [HttpGet("ByStatut/{statutId}")]
        public async Task<IActionResult> GetPersonsByStatut(int statutId)
        {
            try
            {
                var persons = await _personService.GetPersonsByStatut(statutId);
                return Ok(persons);
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
        [Authorize]
        [HttpGet("ByRole/{roleId}")]
        public async Task<IActionResult> GetPersonsByRole(int roleId)
        {
            try
            {
                var persons = await _personService.GetPersonsByRole(roleId);
                return Ok(persons);
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
        [Authorize]
        [HttpGet("ByEmail/{email}")]
        public async Task<IActionResult> GetPersonByEmail(string email)
        {
            try
            {
                var person = await _personService.GetPersonByEmail(email);
                return Ok(person);
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
        [Authorize]
        [HttpPost("verify-password")]
        public async Task<IActionResult> VerifyPassword([FromBody] VerifyPasswordDTO verifyPasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isValid = await _securityService.VerifyCurrentPassword(verifyPasswordDTO.UserId, verifyPasswordDTO.CurrentPassword);
                if (!isValid)
                {
                    return BadRequest("Invalid current password");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [Authorize]
        [HttpPost("uploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            try
            {
                var filePath = await _personService.UploadProfilePictureAsync(file);
                return Ok(new { path = filePath });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
