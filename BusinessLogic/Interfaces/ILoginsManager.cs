using System.Threading.Tasks;
using Models.DataTransferModels;

namespace BusinessLogic.Interfaces
{
    public interface ILoginsManager
    {
        Task<string> LoginUser(UserLogin userLogin);
        Task LogoutUser(string username);
    }
}
