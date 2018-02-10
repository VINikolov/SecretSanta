using System.Threading.Tasks;
using Models.DataTransferModels;

namespace BusinessLogic.Interfaces
{
    public interface IParticipantsManager
    {
        Task AcceptInvite(Participant participant);
    }
}
