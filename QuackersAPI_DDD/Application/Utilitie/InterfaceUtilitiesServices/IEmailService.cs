namespace QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(string email, string tokenResetPassword);

        Task SendPasswordCreatedEmail(string email, string password);
    }
}
