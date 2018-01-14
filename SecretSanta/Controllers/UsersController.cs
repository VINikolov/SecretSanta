using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Interfaces;
using Models;

namespace SecretSanta.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUsersManager _usersManager;

        public UsersController(IUsersManager usersManager)
        {
            _usersManager = usersManager;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateUser(User user)
        {
            await _usersManager.CreateUser(user);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
