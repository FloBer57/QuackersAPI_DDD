namespace QuackersAPI_DDD.API.DTO.AuthDTO
{
    public class UpdateLoggedInDTO
    {
        public string? TokenResetPassword { get; set; }
        public bool IsTemporaryPassword { get; set; }
        public string? LoggedInToken { get; set; }
        public DateTime LoggedInTokenExpirationDat { get; set; }
    }
}
