using System;
using System.Threading.Tasks;
using System.Web;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace BusinessLogic.Implementation
{
    public class LoginsManager : ILoginsManager
    {
        private readonly ILoginsRepository _loginsRepository;
        private readonly IUsersRepository _usersRepository;

        public LoginsManager(ILoginsRepository loginsRepository, IUsersRepository usersRepository)
        {
            _loginsRepository = loginsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<string> LoginUser(UserLogin userLogin)
        {
            var user = await _usersRepository.SelectById(userLogin.Username);

            if (user == null)
            {
                throw new HttpException(404, $"User with username: {userLogin.Username} does not exist.");
            }
            if (user.PasswordHash != userLogin.PasswordHash)
            {
                throw new HttpException(401, "Wrong username or password.");
            }

            userLogin.AuthenticationToken = CreateAuthenticationToken();
            userLogin.Id = Guid.NewGuid();
            await _loginsRepository.Insert(userLogin);

            return userLogin.AuthenticationToken;
        }

        private string CreateAuthenticationToken()
        {
            var guid = Guid.NewGuid();
            return guid.ToString("N");
        }
    }
}
