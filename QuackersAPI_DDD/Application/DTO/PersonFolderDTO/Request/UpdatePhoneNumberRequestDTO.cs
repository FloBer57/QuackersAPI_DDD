using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Request
{
    public class UpdatePhoneNumberRequestDTO
    {
        [Required(ErrorMessage = "Le numéro de téléphone est requis")]
        [Phone(ErrorMessage = "Le numéro de téléphone doit être valide")]
        public string NewPhoneNumber { get; set; }
    }
}
