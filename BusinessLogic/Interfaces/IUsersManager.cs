using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DataTransferModels;

namespace BusinessLogic.Interfaces
{
    public interface IUsersManager
    {
        Task CreateUser(User user);
        Task<User> GetUserByAuthenticationToken(string authenticationToken);
        Task<IEnumerable<User>> GetPagedUsers(int skip, int take, string order, string searchPhrase);
        Task<User> GetUserByUsername(string username);
    }
}
