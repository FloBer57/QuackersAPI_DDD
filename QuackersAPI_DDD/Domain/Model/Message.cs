namespace QuackersAPI_DDD.Domain.Model
{
    public class Message
    {
        public int Message_Id { get; set; }
        public string Message_Text { get; set;}
        public DateTime Message_Date { get; set; } = DateTime.Now;
        public bool Message_IsNotArchivage { get; set; } = true;
        public int Channel_Id { get; set; }
        public int Person_Id { get; set; }

        // Clefs étrangères. 
        public Channel Channel { get; set; }
        public Person Person { get; set; }
    }
}
