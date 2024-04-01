using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.DTO.PersonFolderDTO.ChannelFolderDTO
{
    public class ChannelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int ChannelType_Id { get; set; }

        public ChannelDTO(Channel channel)
        {
            Id = channel.Channel_Id;
            Name = channel.Channel_Name;
            ImagePath = channel.Channel_ImagePath;
            ChannelType_Id = channel.ChannelType_Id;
        }
    }
}
