using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferModels;

namespace DataAccess.Interfaces
{
    public interface IInvitationsRepository : IRepository<GroupInvitation, Guid>
    {
        Task<IEnumerable<GroupInvitation>> GetPagedInvites(string username, int skip, int take, string order);
    }
}
