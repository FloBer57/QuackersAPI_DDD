using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuackersAPI_DDD.Application.DTO;
using QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Request;
using QuackersAPI_DDD.Application.DTO.ChannelFolderDTO.Response;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO;
using QuackersAPI_DDD.Application.DTO.PersonFolderDTO.ChannelFolderDTO;
using QuackersAPI_DDD.Application.Interface;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Domain.Utilitie;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

namespace QuackersAPI_DDD.Application.Service
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository _repository;

        public ChannelService(IChannelRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateChannelResponseDTO> CreateChannel(CreateChannelRequestDTO channelDto)
        {
            var channel = new Channel
            {
                Channel_Name = channelDto.newName,
            };

            await _repository.CreateChannel(channel);
            return new CreateChannelResponseDTO(new ChannelDTO(channel));
        }

        public async Task<GetAllChannelResponseDTO> GetAllChannel()
        {
            var channels = await _repository.GetAllChannel();
            var channelDtos = channels.Select(p => new ChannelDTO(p)).ToList();
            return new GetAllChannelResponseDTO(channelDtos);
        }

        public async Task<GetChannelByIdResponseDTO> GetChannelById(int Id)
        {
            var channel = await _repository.GetChannelById(Id);
            if (channel == null)
            {
                return null;
            }

            var channelDto = new ChannelDTO(channel);
            return new GetChannelByIdResponseDTO(channelDto);
        }
      
        public async Task<UpdateChannelByIdResponseDTO> UpdateName(int id, string newName)
        {
            var channel = await _repository.GetChannelById(id);
            if (channel != null)
            {
                channel.Channel_Name = newName;
                await _repository.UpdateChannel(channel);
                return new UpdateChannelByIdResponseDTO(true, $"Le Nom du channel est désormais {channel.Channel_Name}");
            }
            return new UpdateChannelByIdResponseDTO(false, "Channel non trouvé");
        }

        public async Task<DeleteChannelByIdResponseDTO> DeleteChannel(int id)
        {
            var channel = await _repository.GetChannelById(id);
            if (channel == null)
            {
                return new DeleteChannelByIdResponseDTO(false, "Channel non trouvé");
            }

            await _repository.DeleteChannel(id);
            return new DeleteChannelByIdResponseDTO(true, $"Le channel {channel.Channel_Name} a été supprimé avec succès");
        } 
    }
}
