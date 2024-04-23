using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices
{
    public interface ISecurityService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        string GeneratePassword(int length = 20);
        string GenerateUniqueAttachmentName(string originalFileName);
        Task<string> GeneratePasswordResetToken(Person person);
        Task<bool> ResetPassword(string token, string newPassword);
    }
}
