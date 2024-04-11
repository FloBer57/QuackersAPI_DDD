using QuackersAPI_DDD.API.DTO.ChannelDTO;
using QuackersAPI_DDD.API.DTO.PersonJobTitleDTO;
using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IPersonJobTitleService
    {
        Task<PersonJobTitle> CreatePersonJobTitle(CreatePersonJobTitleDTO createPersonJobTitleDTO);
        Task<IEnumerable<PersonJobTitle>> GetAllPersonJobTitle();
        Task<PersonJobTitle> GetPersonJobTitleById(int id);
        Task<PersonJobTitle> UpdatePersonJobTitle(int id, UpdatePersonJobTitleDTO updatePersonJobTitleDTO);
        Task<bool> DeletePersonJobTitle(int id);
    }
}
