namespace QuackersAPI_DDD.Application.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Request;
    using QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Response;

    public interface IChannelService
    {
        Task<CreateChannelResponseDTO> CreateChannel(CreateChannelRequestDTO personDto);
        Task<GetAllChannelResponseDTO> GetAllChannel();
        Task<GetChannelByIdResponseDTO> GetChannelById(int Id);
        Task<UpdateChannelByIdResponseDTO> UpdateName(int id, string newName);
        Task<DeleteChannelByIdResponseDTO> DeleteChannel(int id);
        
    }
}
