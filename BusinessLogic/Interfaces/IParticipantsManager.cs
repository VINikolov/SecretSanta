using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferModels;

namespace BusinessLogic.Interfaces
{
    public interface IParticipantsManager
    {
        Task AcceptInvite(Participant participant);
        Task<IEnumerable<Participant>> GetGroupsForUser(string username, int skip, int take);
    }
}
