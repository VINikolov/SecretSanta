using System.Threading.Tasks;
using Models.DataTransferModels;

namespace BusinessLogic.Interfaces
{
    public interface IGroupsManager
    {
        Task CreateGroup(Group group);
    }
}
