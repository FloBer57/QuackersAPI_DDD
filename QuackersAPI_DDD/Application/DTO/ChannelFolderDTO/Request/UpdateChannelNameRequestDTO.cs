using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Request
{
    public class UpdateChannelNameRequestDTO
    {
        [Required(ErrorMessage = "Le nom est nécéssaire.")]
        public string NewName { get; set; }
    }
}
