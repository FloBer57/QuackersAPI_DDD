using System.ComponentModel.DataAnnotations;

namespace QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Request
{
    public class CreateChannelRequestDTO
    {
        [Required(ErrorMessage = "Le nom est requis")]
        [StringLength(50, ErrorMessage = "Le prénom ne peut pas dépasser 50 caractère")]
        public string newName { get; set; }

        // [Required(ErrorMessage = "Le Job doit être renseigné")]
        // public int JobTitleId { get; set; }
    }
}
