namespace QuackersAPI_DDD.Domain.Model
{
    public class ResetTokenPassword
    {
        public int Token_Id { get; set; }
        public string Token { get; set; }
        public int Person_Id { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Person Person { get; set; }
    }
}
