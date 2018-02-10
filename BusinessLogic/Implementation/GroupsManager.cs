using System;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace BusinessLogic.Implementation
{
    public class GroupsManager : IGroupsManager
    {
        private readonly IGroupsRepository _groupsRepository;
        private readonly IParticipantsRepository _participantsRepository;

        public GroupsManager(IGroupsRepository groupsRepository, IParticipantsRepository participantsRepository)
        {
            _groupsRepository = groupsRepository;
            _participantsRepository = participantsRepository;
        }

        public async Task CreateGroup(Group group)
        {
            await _groupsRepository.Insert(group);
            var admin = new Participant
            {
                Id = Guid.NewGuid(),
                GroupName = group.GroupName,
                Name = group.Admin
            };
            await _participantsRepository.Insert(admin);
        }
    }
}
