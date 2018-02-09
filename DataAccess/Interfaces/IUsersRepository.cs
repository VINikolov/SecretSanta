using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferModels;

namespace DataAccess.Interfaces
{
    public interface IUsersRepository : IRepository<User, string>
    {
        Task<User> GetUserByAuthenticationToken(string authenticationToken);
        Task<IEnumerable<User>> GetPagedUsers(int skip, int take, string order, string searchPhrase);
    }
}
