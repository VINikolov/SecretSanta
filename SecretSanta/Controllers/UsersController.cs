using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Interfaces;
using Models.DataTransferModels;
using Newtonsoft.Json.Linq;

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

            var jsonString = "{displayName : '" + user.Displayname + "'}";
            var json = JObject.Parse(jsonString).ToString();

            var response =
                new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(json) };
            return response;
        }
    }
}
