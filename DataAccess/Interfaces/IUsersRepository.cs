using System.Threading.Tasks;
using Models.DataTransferModels;

namespace DataAccess.Interfaces
{
    public interface IUsersRepository : IRepository<User, string>
    {
        Task<User> GetUserByAuthenticationToken(string authenticationToken);
    }
}
