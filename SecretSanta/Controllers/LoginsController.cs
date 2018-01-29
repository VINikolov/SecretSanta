using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Interfaces;
using Models.DataTransferModels;
using Newtonsoft.Json.Linq;

namespace SecretSanta.Controllers
{
    public class LoginsController : ApiController
    {
        private readonly ILoginsManager _loginsManager;

        public LoginsController(ILoginsManager loginsManager)
        {
            _loginsManager = loginsManager;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Login(UserLogin userLogin)
        {
            var authToken = await _loginsManager.LoginUser(userLogin);

            var jsonString = "{authenticationToken : '" + authToken + "'}";
            var json = JObject.Parse(jsonString).ToString();

            var response = new HttpResponseMessage(HttpStatusCode.Created) {Content = new StringContent(json)};
            return response;
        }

        [HttpDelete]
        [Route("api/Logins/{username}")]
        public async Task Logout(string username)
        {
            await _loginsManager.LogoutUser(username);
        }
    }
}
