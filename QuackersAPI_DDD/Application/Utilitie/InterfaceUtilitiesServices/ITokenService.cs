using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices
{
    public interface ITokenService
    {
        string GenerateToken(Person user);
    }
}
