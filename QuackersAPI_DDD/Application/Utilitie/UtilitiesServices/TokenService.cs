using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices;
using QuackersAPI_DDD.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuackersAPI_DDD.Application.Utilitie.UtilitiesServices
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        public TokenService(string secretKey, string issuer, string audience)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
        }
        public string GenerateToken(Person user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User object is null");
            }

            if (string.IsNullOrEmpty(_secretKey) || string.IsNullOrEmpty(_issuer) || string.IsNullOrEmpty(_audience))
            {
                throw new InvalidOperationException("JWT configuration settings are not properly initialized.");
            }

            if (user.PersonRole == null)
            {
                throw new InvalidOperationException("Le rôle de l'utilisateur n'est pas défini.");
            }
            // Log des valeurs pour le débogage
            Console.WriteLine($"Generating token for user: {user.Person_Id}");
            Console.WriteLine($"Issuer: {_issuer}, Audience: {_audience}, Key: {_secretKey.Substring(0, 5)}...");  // Show only a part of the key for security

            var claims = new List<Claim>
{
        new Claim(ClaimTypes.NameIdentifier, user.Person_Id.ToString()),
        new Claim(ClaimTypes.Email, user.Person_Email),
        new Claim(ClaimTypes.Role, user.PersonRole.PersonRole_Name)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
