using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using System;
using System.Linq;

namespace QuackersAPI_DDD.Application.Utilitie.UtilitiesServices
{
    public class SecurityService : ISecurityService
    {
        /*private readonly IEmailService _emailService; */
        private readonly IResetTokenPasswordRepository _resetTokenRepository; 
        private readonly IPersonRepository _personRepository;
        private static readonly Random random = new Random();

        public SecurityService(/*IEmailService emailService ,*/IResetTokenPasswordRepository resetTokenRepository, IPersonRepository personRepository)
        {
            /*_emailService = emailService;*/
            _resetTokenRepository = resetTokenRepository;
            _personRepository = personRepository;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public string GeneratePassword(int length = 20)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string GenerateUniqueAttachmentName(string originalFileName)
        {
            string fileExtension = Path.GetExtension(originalFileName);
            string uniqueName = $"{Guid.NewGuid()}{fileExtension}";
            return uniqueName;
        }

        public async Task<string> GeneratePasswordResetToken(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person), "Person object is null.");
            }

            var token = Guid.NewGuid().ToString();
            var resetToken = new ResetTokenPassword
            {
                Token = token,
                Person_Id = person.Person_Id,
                ExpiresAt = DateTime.Now.AddHours(1) 
            };

            await _resetTokenRepository.AddAsync(resetToken);
            await _resetTokenRepository.SaveChangesAsync();

            return token;
        }


        public async Task<bool> ResetPassword(string token, string newPassword)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token cannot be null or empty.", nameof(token));
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ArgumentException("New password cannot be null or empty.", nameof(newPassword));
            }

            var resetToken = await _resetTokenRepository.FindAsync(t => t.Token == token && t.ExpiresAt > DateTime.UtcNow);
            if (resetToken == null)
            {
                throw new KeyNotFoundException("Token not found or expired.");
            }

            var person = await _personRepository.GetPersonById(resetToken.Person_Id);
            if (person == null)
            {
                throw new KeyNotFoundException("Person not found for the provided token.");
            }

            person.Person_Password = HashPassword(newPassword); 
            await _personRepository.UpdatePerson(person);

            return true;
        }


    }
}
