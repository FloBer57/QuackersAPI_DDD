using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.PersonXMessageDTO
{
    public class CreatePersonXMessageDTO
    {
        [Required(ErrorMessage = "Person_Id is required.")]
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Message_Id is required.")]
        public int MessageId { get; set; }
    }
}
