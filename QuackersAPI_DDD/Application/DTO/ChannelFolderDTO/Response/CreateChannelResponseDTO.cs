using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.ChannelFolderDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Response
{
    public class CreateChannelResponseDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public CreateChannelResponseDTO(ChannelDTO channelDto)
        {
            Message = $"Le channel {channelDto.Name} à été créé.";
        }
    }
}
