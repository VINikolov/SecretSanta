using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace BusinessLogic.Implementation
{
    public class UsersManager : IUsersManager
    {
        private readonly IUsersRepository _usersRepository;

        public UsersManager(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task CreateUser(User user)
        {
            await _usersRepository.Insert(user);
        }

        public async Task<User> GetUserByAuthenticationToken(string authenticationToken)
        {
            return await _usersRepository.GetUserByAuthenticationToken(authenticationToken);
        }
    }
}
