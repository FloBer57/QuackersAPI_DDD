using Microsoft.AspNetCore.Mvc;
using QuackersAPI_DDD.API.DTO.AuthenticationDTO;
using QuackersAPI_DDD.Application.Interface; // Interfaces for services
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository; // Security and Token interfaces

namespace QuackersAPI_DDD.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IPersonService _personService;
        private readonly ISecurityService _securityService;
        private readonly ITokenJwtService _tokenService;
        private readonly IRefreshTokenService _refreshTokenService; 

        public AuthenticationController(
            IPersonService personService,
            ISecurityService securityService,
            ITokenJwtService tokenService,
            IRefreshTokenService refreshTokenService,
            IEmailService emailService) 
        {
            _personService = personService;
            _securityService = securityService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                return BadRequest("Email and password are required.");
            }

            try
            {
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


                var refreshToken = _refreshTokenService.GenerateRefreshToken(person); 
                return Ok(new { Token = token, RefreshToken = refreshToken, Message = "Login successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpPost("reset-password-request")]
        public async Task<IActionResult> ResetPasswordRequest([FromBody] ResetPasswordRequestDTO request)
        {
            try
            {
                var person = await _personService.GetPersonByEmail(request.Email);
                if (person == null)
                {
                    return NotFound("User not found.");
                }

                var resetToken = _securityService.GeneratePasswordResetToken(person);
                await _emailService.SendPasswordResetEmail(person.Person_Email, resetToken.Result);

                return Ok(new { Message = "Reset password link has been sent to your email.", ResetToken = resetToken });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred. Please try again later. " + ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            try
            {
                var result = await _securityService.ResetPassword(resetPasswordDTO.Token, resetPasswordDTO.NewPassword);
                    return Ok("Password has been reset successfully.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred. Please try again later. " + ex.Message);
            }
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var personId = await _refreshTokenService.ValidateRefreshToken(refreshTokenDTO.RefreshToken);
                var person = await _personService.GetPersonById((int)personId);
                var token = _tokenService.GenerateToken(person);
                var newRefreshToken = await _refreshTokenService.GenerateRefreshToken(person);
                return Ok(new { Token = token, RefreshToken = newRefreshToken });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal error occurred. Please try again later. " + ex.Message);
            }
        }


        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeTokenDTO revokeTokenDTO)
        {
            try
            {
                var success = await _refreshTokenService.RevokeRefreshToken(revokeTokenDTO.RefreshToken);
                return Ok(new { Success = success, Message = "Token has been successfully revoked." });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

    }
}