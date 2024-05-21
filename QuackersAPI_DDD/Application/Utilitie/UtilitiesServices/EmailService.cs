using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;
using QuackersAPI_DDD.Domain.Utilitie.Model;
using Org.BouncyCastle.Crypto.Tls;

namespace QuackersAPI_DDD.Application.Utilitie.UtilitiesServices;
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendPasswordResetEmail(string email, string TokenResetPassword)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = "Récupération de mot de passe de Quackers";

        string body = $"<h1>Réinitialisation du mot de passe</h1>" +
                      $"<p>Veuillez utiliser le lien suivant pour réinitialiser votre mot de passe :</p>" +
                      $"<a href='http://localhost:3000/login?token={TokenResetPassword}'>Réinitialiser le mot de passe</a>";

        emailMessage.Body = new TextPart("html") { Text = body };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }

    public async Task SendPasswordCreatedEmail(string email, string Password)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = "Création de compte sur Quackers";

        string body = $"<h1>Création du compte</h1>" +
                      $"<p>Veuillez utiliser le lien suivant pour vous connecter la première fois.</p>" +
                      $"<a href='http://localhost:3000/login'> Voici votre mot de passe temporaire ! {Password} </a>";

        emailMessage.Body = new TextPart("html") { Text = body };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
