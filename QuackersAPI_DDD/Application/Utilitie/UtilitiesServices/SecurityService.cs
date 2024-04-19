using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;
using System;
using System.Linq;

namespace QuackersAPI_DDD.Application.Utilitie.UtilitiesServices
{
    public class SecurityService : ISecurityService
    {
        private static readonly Random random = new Random();

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
    }
}
