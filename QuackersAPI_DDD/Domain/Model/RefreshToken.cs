namespace QuackersAPI_DDD.Domain.Model
{
    public class RefreshToken
    {
        public int Token_Id { get; set; }
        public int Person_Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Revoked { get; set; }

        // Navigation property for related Person entity
        public virtual Person Person { get; set; }
    }
}
