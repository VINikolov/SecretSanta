using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
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

        public async Task<IEnumerable<User>> GetPagedUsers(int skip, int take, string order, string searchPhrase)
        {
            return await _usersRepository.GetPagedUsers(skip, take, order, searchPhrase);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _usersRepository.SelectById(username);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return await _usersRepository.SelectById(username);
        }
    }
}
