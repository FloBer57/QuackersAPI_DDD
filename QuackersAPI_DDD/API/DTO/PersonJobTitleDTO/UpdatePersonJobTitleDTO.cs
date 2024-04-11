using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.PersonJobTitleDTO
{
    public class UpdatePersonJobTitleDTO
    {
        [Required(ErrorMessage = "Job Title name is required.")]
        [StringLength(255, ErrorMessage = "Job Title name must be at most 255 characters long.")]
        public string JobTitle_Name { get; set; }
    }
}
