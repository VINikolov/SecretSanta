using System;
using Models.DataTransferModels;

namespace DataAccess.Interfaces
{
    public interface IParticipantsRepository : IRepository<Participant, Guid>
    {
    }
}
