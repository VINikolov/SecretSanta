using System;
using System.Threading.Tasks;
using Models.DataTransferModels;

namespace BusinessLogic.Interfaces
{
    public interface IInvitationsManager
    {
        Task<Guid> CreateInvitation(GroupInvitation invitation);
    }
}
