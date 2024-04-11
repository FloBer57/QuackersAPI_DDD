using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.PersonStatutDTO
{
    public class CreatePersonStatutDTO
    {
        [Required(ErrorMessage = "Statut name is required.")]
        [StringLength(50, ErrorMessage = "Statut name must be at most 50 characters long.")]
        public string PersonStatut_Name { get; set; }
    }
}
