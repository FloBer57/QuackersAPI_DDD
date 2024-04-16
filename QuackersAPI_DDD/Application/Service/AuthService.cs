using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Utilitie;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using System.IdentityModel.Tokens.Jwt;

namespace QuackersAPI_DDD.Application.Service
{
    public class AuthService : IAuthService
    {
        private readonly IPersonRepository _personRepository;

        public AuthService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<(bool Success, string Token)> LoginAsync(string email, string password)
        {
            var person = await _personRepository.GetPersonByEmail(email);
            if (person == null || !SecurityService.VerifyPassword(password, person.Person_Password))
                return (false, null);

            if (person.Person_IsTemporaryPassword)
                return (true, null); 

            var token = SecurityService.GenerateToken();
            return (true, token);
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var person = await _personRepository.GetPersonByEmail(email);
            if (person == null || !person.Person_IsTemporaryPassword)
                return false;

            person.Person_Password = SecurityService.HashPassword(newPassword);
            person.Person_IsTemporaryPassword = false;
            await _personRepository.UpdatePerson(person);
            return true;
        }
    }
}
