namespace QuackersAPI_DDD.Domain.Model
{
    public class Channel
    {
        public int Channel_Id { get; set; }
        public string Channel_Name { get; set;}
        public string Channel_ImagePath { get; set; } = "path/to/image/channel";
        public int ChannelType_Id { get; set; } = 1;
    }
}
