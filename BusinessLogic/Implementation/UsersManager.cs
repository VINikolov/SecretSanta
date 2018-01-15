using System;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models;

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
            user.Id = Guid.NewGuid();
            user.AuthenticationToken = CreateAuthenticationToken();
            await _usersRepository.Insert(user);
        }

        private string CreateAuthenticationToken()
        {
            var guid = Guid.NewGuid();
            return guid.ToString("N");
        }
    }
}
