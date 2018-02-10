using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferModels;

namespace BusinessLogic.Interfaces
{
    public interface IInvitationsManager
    {
        Task<Guid> CreateInvitation(GroupInvitation invitation);
        Task<IEnumerable<GroupInvitation>> GetPagedInvites(string username, int skip, int take, string order);
    }
}
