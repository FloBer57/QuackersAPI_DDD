namespace QuackersAPI_DDD.API.DTO.ChannelDTO
{
    public class CreateChannelDTO
    {
        public string Channel_Name { get; set; }
        public string? Channel_ImagePath { get; set; }
        public int ChannelType_Id { get; set; }
    }

}
