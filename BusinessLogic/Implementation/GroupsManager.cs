using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace BusinessLogic.Implementation
{
    public class GroupsManager : IGroupsManager
    {
        private readonly IGroupsRepository _groupsRepository;

        public GroupsManager(IGroupsRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }

        public async Task CreateGroup(Group group)
        {
            await _groupsRepository.Insert(group);
        }
    }
}
