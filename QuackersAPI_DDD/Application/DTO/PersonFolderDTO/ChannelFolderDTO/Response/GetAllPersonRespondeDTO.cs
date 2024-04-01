using QuackersAPI_DDD.Application.DTO.PersonFolderDTO;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.ChannelFolderDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.DTO.PersonFolderDTO.Response
{
    public class GetAllChannelResponseDTO
    {
        public IEnumerable<ChannelDTO> Channels { get; set; }
        public string Message { get; set; }
        public int NumberOfChannel { get; set; }

        public GetAllChannelResponseDTO(IEnumerable<ChannelDTO> channel)
        {
            Channels = channel;
            foreach (var item in channel)
            {
                NumberOfChannel++;
            }
            Message = $"Vous avez récupéré {NumberOfChannel} Channel.";

        }
    }
}