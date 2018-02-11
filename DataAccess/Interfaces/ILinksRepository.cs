using System;
using System.Threading.Tasks;
using Models.DataTransferModels;

namespace DataAccess.Interfaces
{
    public interface ILinksRepository : IRepository<Link, Guid>
    {
        Task<Link> SelectByParams(string username, string groupName);
    }
}
