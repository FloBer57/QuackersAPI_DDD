using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.API.DTO.PersonXChannelDTO;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using QuackersAPI_DDD.Infrastructure.Repository.QuackersAPI_DDD.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuackersAPI_DDD.Application.Service
{
    public class PersonXChannelService : IPersonXChannelService
    {
        private readonly IPersonXChannelRepository _repository;
        private readonly IPersonRepository _personRepository;
        private readonly IChannelRepository _channelRepository;

        public PersonXChannelService(IPersonXChannelRepository repository, IPersonRepository personRepository, IChannelRepository channelRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
            _channelRepository = channelRepository;
        }

        public async Task<IEnumerable<PersonXChannel>> GetAllAssociations()
        {
            return await _repository.GetAllAssociations() ?? new List<PersonXChannel>();
        }

        public async Task<PersonXChannel> GetAssociationById(int personId, int channelId)
        {
            var association = await _repository.GetAssociationById(personId, channelId);
            if (association == null)
                throw new KeyNotFoundException($"Association not found with person ID {personId} and channel ID {channelId}.");
            return association;
        }

        public async Task<IEnumerable<Person>> GetPersonsByChannelId(int channelId)
        {
            var persons = await _repository.GetPersonsByChannelId(channelId);
            if (persons == null)
            {
                throw new KeyNotFoundException("No person found in this channel");
            }
            return persons;
        }

        public async Task<IEnumerable<Channel>> GetChannelsByPersonId(int personId)
        {
            var channels = await _repository.GetChannelsByPersonId(personId);
            if (channels == null)
            {
                throw new KeyNotFoundException("No channels found for this person");
            }
            return channels;
        }

        public async Task<PersonXChannel> CreateAssociation(CreatePersonXChannelDTO dto)
        {
            var checkPerson = await _personRepository.GetPersonById(dto.PersonId);
            if (checkPerson == null)
                throw new KeyNotFoundException($"Person ID {dto.PersonId} does not exist.");

            var checkChannel = await _channelRepository.GetChannelById(dto.ChannelId);
            if (checkChannel == null)
                throw new KeyNotFoundException($"Channel ID {dto.ChannelId} does not exist.");

            var existing = await _repository.GetAssociationById(dto.PersonId, dto.ChannelId);
            if (existing != null)
                throw new InvalidOperationException("An association already exists between the specified person and channel.");

            var newAssociation = new PersonXChannel
            {
                Person_Id = dto.PersonId,
                Channel_Id = dto.ChannelId,
                PersonXchannelSignInDate = DateTime.Now,
            };

            return await _repository.CreateAssociation(newAssociation);
        }

        public async Task<PersonXChannel> UpdateAssociation(int personId, int channelId, UpdatePersonXChannelDTO dto)
        {
            var association = await GetAssociationById(personId, channelId);
            if (association == null)
            {
                throw new KeyNotFoundException("Can't update the PersonXChannel cause personId, channelId not found..");
            }

            association.PersonXchannelSignInDate = dto.SignInDate ?? association.PersonXchannelSignInDate;
            return await _repository.UpdateAssociation(association);
        }

        public async Task<bool> DeleteAssociation(int personId, int channelId)
        {
            var checkPerson = await _personRepository.GetPersonById(personId);
            if (checkPerson == null)
                throw new KeyNotFoundException($"Person ID {personId} does not exist.");

            var checkChannel = await _channelRepository.GetChannelById(channelId);
            if (checkChannel == null)
                throw new KeyNotFoundException($"Channel ID {channelId} does not exist.");

            await _repository.DeleteAssociation(personId, channelId);
            return true;
        }
    }
}
