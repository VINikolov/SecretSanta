using System;
using Models.DataTransferModels;

namespace DataAccess.Interfaces
{
    public interface IInvitationsRepository : IRepository<GroupInvitation, Guid>
    {
    }
}
