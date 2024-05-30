using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.PersonDTO
{
    public class UpdatePersonDTO
    {
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number need 10 characters")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        public string? Description { get; set; }
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters.")]
        
        public string? ProfilPicturePath { get; set; } 
        public string? Password { get; set; }
        public int? JobTitleId { get; set; }
        public int? RoleId { get; set; }
        public int? StatutId { get; set; }

    }

}
