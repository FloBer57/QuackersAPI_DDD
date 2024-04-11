using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.PersonDTO
{
    public class PersonDTO
    {
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string? Description { get; set; }

        public string ProfilPicturePath { get; set; } = "Path/To/Default/Image";
    }

}
