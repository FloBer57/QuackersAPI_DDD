namespace QuackersAPI_DDD.API.DTO.AuthenticationDTO
{
    public class ResetPasswordDTO
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
