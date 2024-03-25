using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Request
{
    public class UpdatePasswordRequestDTO
    {
        [Required(ErrorMessage = "Le mot de passe est nécéssaire.")]
        public string NewPassword { get; set; }
    }
}
