using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.AuthenticationDTO;
using QuackersAPI_DDD.Application.Interface; // Interfaces for services
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices; // Security and Token interfaces

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ISecurityService _securityService;
        private readonly ITokenService _tokenService;

        public AuthenticationController(IPersonService personService, ISecurityService securityService, ITokenService tokenService)
        {
            _personService = personService;
            _securityService = securityService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var person = await _personService.GetPersonByEmail(loginDTO.Email);
            if (person == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            bool isPasswordCorrect = _securityService.VerifyPassword(loginDTO.Password, person.Person_Password);
            if (!isPasswordCorrect)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = _tokenService.GenerateToken(person);

            return Ok(new { Token = token, Message = "Login successful." });
        }
    }
}
