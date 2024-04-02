using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.ChannelFolderDTO;

namespace QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Response
{
    public class GetChannelByIdResponseDTO
    {
        public ChannelDTO ChannelDto { get; set; }
        public string Message { get; set; }

        public GetChannelByIdResponseDTO(ChannelDTO channelDto)
        {
            ChannelDto = channelDto;
            Message = channelDto != null
                ? $"Le channel {channelDto.Name} a été récupéré avec succès."
                : "Channel non trouvé.";
        }
    }
}