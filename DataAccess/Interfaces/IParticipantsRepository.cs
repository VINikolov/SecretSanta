using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferModels;

namespace DataAccess.Interfaces
{
    public interface IParticipantsRepository : IRepository<Participant, Guid>
    {
        Task<IEnumerable<Participant>> SelectGroupsForUser(string username, int skip, int take);
    }
}
