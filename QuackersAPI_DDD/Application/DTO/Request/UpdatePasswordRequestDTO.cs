using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.Application.DTO.Request
{
    public class UpdatePasswordRequestDTO
    {
        [Required(ErrorMessage = "Le mot de passe est nécéssaire.")]
        public string NewPassword { get; set; }
    }
}
