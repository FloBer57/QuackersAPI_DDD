using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuackersAPI_DDD.API.DTO.ChannelPersonRoleXPersonXChannel;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;

public class ChannelPersonRoleXPersonXChannelService : IChannelPersonRoleXPersonXChannelService
{
    private readonly IChannelPersonRoleXPersonXChannelRepository _repository;
    private readonly IPersonRepository _personRepository;
    private readonly IChannelRepository _channelRepository;
    private readonly IChannelPersonRoleRepository _roleRepository;

    public ChannelPersonRoleXPersonXChannelService(
        IChannelPersonRoleXPersonXChannelRepository repository,
        IPersonRepository personRepository,
        IChannelRepository channelRepository,
        IChannelPersonRoleRepository roleRepository)
    {
        _repository = repository;
        _personRepository = personRepository;
        _channelRepository = channelRepository;
        _roleRepository = roleRepository;
    }

    public async Task<IEnumerable<ChannelPersonRoleXPersonXChannel>> GetAllAssociations()
    {
        var association = await _repository.GetAllAssociations();
        return association ?? new List<ChannelPersonRoleXPersonXChannel>();
    }

    public async Task<ChannelPersonRoleXPersonXChannel> GetAssociationByIds(int personId, int channelId)
    {
        var association = await _repository.GetAssociationByIds(personId, channelId);
        if (association == null)
            throw new KeyNotFoundException("Association not found.");
        return association;
    }

    public async Task<IEnumerable<Person>> GetPersonsByRoleInChannel(int channelId, int roleId)
    {
        var association = await _repository.GetPersonsByRoleInChannel(channelId, roleId);
        if (association == null)
        {
            throw new KeyNotFoundException($"No association find with this channelId {channelId} and roleId {roleId}.");
        }
        return association;
    }

    public async Task<IEnumerable<ChannelPersonRole>> GetRolesByPersonInChannels(int personId)
    {
        var roles = await _repository.GetRolesByPersonInChannels(personId);
        if (roles == null)
        {
            throw new KeyNotFoundException("No roles found with person.");
        }
        return roles;
    }

    public async Task <ChannelPersonRoleXPersonXChannel> GetRolesByPersonInOneChannels(int personId, int channelId)
    {
        var role = await _repository.GetRolesByPersonInOneChannel(personId, channelId);
        if (role == null)
        {
            throw new KeyNotFoundException("No roles found with person.");
        }
        return role;
    }

    public async Task<ChannelPersonRoleXPersonXChannel> CreateAssociation(CreateChannelPersonRoleXPersonXChannelDTO dto)
    {
        var person = await _personRepository.GetPersonById(dto.PersonId);
        var channel = await _channelRepository.GetChannelById(dto.ChannelId);
        var role = await _roleRepository.GetChannelPersonRoleById(dto.ChannelPersonRole_Id);

        if (person == null) throw new KeyNotFoundException("Person not found.");
        if (channel == null) throw new KeyNotFoundException("Channel not found.");
        if (role == null) throw new KeyNotFoundException("Role not found.");

        var newAssociation = new ChannelPersonRoleXPersonXChannel
        {
            Person_Id = dto.PersonId,
            Channel_Id = dto.ChannelId,
            ChannelPersonRole_Id = dto.ChannelPersonRole_Id,
            ChannelPersonRoleXpersonXchannelAffectDate = DateTime.Now,
        };

        var createdAssociation = await _repository.CreateAssociation(newAssociation);
        if (createdAssociation == null)
            throw new InvalidOperationException("Failed to create association.");

        return createdAssociation;
    }

    public async Task<ChannelPersonRoleXPersonXChannel> UpdateAssociation(int personId, int channelId, UpdateChannelPersonRoleXPersonXChannelDTO dto)
    {
        var association = await GetAssociationByIds(personId, channelId);
        if (association == null) throw new KeyNotFoundException("Association not found.");

        var role = await _roleRepository.GetChannelPersonRoleById(dto.ChannelPersonRoleId);
        if (role == null) throw new KeyNotFoundException("Role not found.");

        association.ChannelPersonRole = role;

        var updatedAssociation = await _repository.UpdateAssociation(association);
        if (updatedAssociation == null)
            throw new InvalidOperationException("Failed to update association.");

        return updatedAssociation;
    }

    public async Task<bool> DeleteAssociation(int personId, int channelId)
    {
        var deleted = await _repository.DeleteAssociation(personId, channelId);
        if (!deleted)
            throw new KeyNotFoundException("No association found to delete.");
        return true;
    }
}
