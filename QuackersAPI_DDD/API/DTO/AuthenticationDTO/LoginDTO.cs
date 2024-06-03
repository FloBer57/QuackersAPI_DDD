using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.AuthenticationDTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email must be at most 100 characters long.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be at most 100 characters long.")]
        public string? Password { get; set; }
    }
}
