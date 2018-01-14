using System.Threading.Tasks;
using Models;

namespace BusinessLogic.Interfaces
{
    public interface IUsersManager
    {
        Task CreateUser(User user);
    }
}
