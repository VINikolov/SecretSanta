using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
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
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            if (user.PasswordHash != userLogin.PasswordHash)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            userLogin.AuthenticationToken = CreateAuthenticationToken();
            await _loginsRepository.Insert(userLogin);

            return userLogin.AuthenticationToken;
        }

        public async Task LogoutUser(string username)
        {
            var userLogin = await _loginsRepository.SelectById(username);

            if (userLogin == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            await _loginsRepository.Delete(username);
        }

        private string CreateAuthenticationToken()
        {
            var guid = Guid.NewGuid();
            return guid.ToString("N");
        }
    }
}
