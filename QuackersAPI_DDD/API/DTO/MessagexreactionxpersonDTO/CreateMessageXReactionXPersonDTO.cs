using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.MessagexreactionxpersonDTO
{
    public class CreateMessageXReactionXPersonDTO
    {
        [Required(ErrorMessage = "Person_Id is required.")]
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Message_Id is required.")]
        public int MessageId { get; set; }
        [Required(ErrorMessage = "Reaction_Id is required.")]
        public int ReactionId { get; set; }
    }
}
