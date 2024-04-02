namespace QuackersAPI_DDD.Domain.Model
{
    public class ChannelType
    {
        public int ChannelType_Id { get; set; }
        public string ChannelType_Name { get; set; }

        public ICollection<Channel> Channels { get; set; }
    }
}
