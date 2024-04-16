namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IAuthService
    {
        Task<(bool Success, string Token)> LoginAsync(string email, string password);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
    }
}
