using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Interfaces;
using Models.ApiResponseModels;
using Models.DataTransferModels;
using Newtonsoft.Json;
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
            var loginResponse = new LoginResponse
            {
                AuthenticationToken = authToken
            };
            var jsonResponse = JsonConvert.SerializeObject(loginResponse);

            var response = new HttpResponseMessage(HttpStatusCode.Created) {Content = new StringContent(jsonResponse)};
            return response;
        }

        [HttpDelete]
        [Route("api/Logins/{username}")]
        public async Task<HttpResponseMessage> Logout(string username)
        {
            await _loginsManager.LogoutUser(username);

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
