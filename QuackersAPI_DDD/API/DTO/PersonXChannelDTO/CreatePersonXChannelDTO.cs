using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.API.DTO.PersonXChannelDTO
{
    public class CreatePersonXChannelDTO
    {
        [Required(ErrorMessage = "Person_Id is required.")]
        public int PersonId { get; set; }
        [Required(ErrorMessage = "Channel_Id is required.")]
        public int ChannelId { get; set; }
    }
}
