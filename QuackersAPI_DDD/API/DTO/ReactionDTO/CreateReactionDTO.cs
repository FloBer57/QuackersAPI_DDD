using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.ReactionDTO
{
    public class CreateReactionDTO
    {
        [Required(ErrorMessage = "Reaction name is required.")]
        [StringLength(50, ErrorMessage = "Statut name must be at most 50 characters long.")]
        public string ReactionName { get; set; }
        public string ReactionPicturePath { get; set; }
    }
}
