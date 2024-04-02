using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Request
{
    public class UpdatePersonPhoneNumberRequestDTO
    {
        [Required(ErrorMessage = "Le numéro de téléphone est requis")]
        [Phone(ErrorMessage = "Le numéro de téléphone doit être valide")]
        public string NewPhoneNumber { get; set; }
    }
}
