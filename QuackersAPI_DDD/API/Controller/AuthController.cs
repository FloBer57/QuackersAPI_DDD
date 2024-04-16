using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.AuthDTO;
using QuackersAPI_DDD.API.DTO.LoginDTO;
using QuackersAPI_DDD.API.DTO.PersonDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Utilitie;

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPersonService _personService;

        public AuthController(IPersonService personService)
        {
            _personService = personService;
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
                return Unauthorized("Invalid Credentials");
            }

            bool isPasswordCorrect = SecurityService.VerifyPassword(loginDTO.Password, person.Person_Password);
            if (!isPasswordCorrect)
            {
                return Unauthorized("Invalid Credentials");
            }

            if (person.Person_IsTemporaryPassword)
            {
                return Ok(new
                {
                    Message = "Temporary password needs to be reset",
                    MustResetPassword = true,
                    Token = SecurityService.GenerateToken()
                });
            }
            return Ok(new
            {
                Message = "Login successful",
                Token = SecurityService.GenerateToken()
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            if (string.IsNullOrWhiteSpace(resetPasswordDTO.Email) || string.IsNullOrWhiteSpace(resetPasswordDTO.NewPassword))
            {
                return BadRequest("Email and new password are required.");
            }

            var person = await _personService.GetPersonByEmail(resetPasswordDTO.Email);
            if (person == null || !person.Person_IsTemporaryPassword)
            {
                return BadRequest("Invalid request or no temporary password set.");
            }

            person.Person_Password = SecurityService.HashPassword(resetPasswordDTO.NewPassword);
            person.Person_IsTemporaryPassword = false;

            await _personService.UpdatePerson(person.Person_Id, new UpdatePersonDTO
            {
                Password = resetPasswordDTO.NewPassword,
            });

            return Ok("Password reset successfully.");
        }

        /* Déconnexion à gérer dans le Front par un kill token */
    }

}
