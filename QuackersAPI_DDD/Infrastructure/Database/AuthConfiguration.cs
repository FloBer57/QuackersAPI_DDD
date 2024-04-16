/*using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace QuackersAPI_DDD.Infrastructure.Database
{
    public class AuthConfiguration
    {
        public SymmetricSecurityKey SigningKey { get; }

        public AuthConfiguration(IConfiguration configuration)
        {
            var jwtKey = configuration["Jwt:Key"];
            SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        }
    }
}
*/
