using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.Utilitie.InterfaceUtilitiesServices
{
    public interface ITokenJwtService
    {
        string GenerateToken(Person person);
    }
}
