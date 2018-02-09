using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLogic.Interfaces;
using Models.ApiResponseModels;
using Models.DataTransferModels;
using Newtonsoft.Json;
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

        [HttpGet]
        public async Task<HttpResponseMessage> GetPagedUsers(int skip, int take, string order, string searchPhrase = null)
        {
            var users = await _usersManager.GetPagedUsers(skip, take, order, searchPhrase);
            var usersResponseModels = Mapper.Map<List<UserResponse>>(users);
            var jsonUsers = JsonConvert.SerializeObject(usersResponseModels);

            var response = new HttpResponseMessage { Content = new StringContent(jsonUsers) };
            return response;
        }

        [HttpGet]
        [Route("api/Users/{username}")]
        public async Task<HttpResponseMessage> GetUser(string username)
        {
            var user = await _usersManager.GetUserByUsername(username);
            var userResponseModel = Mapper.Map<UserResponse>(user);
            var jsonUser = JsonConvert.SerializeObject(userResponseModel);

            var response = new HttpResponseMessage { Content = new StringContent(jsonUser) };
            return response;
        }
    }
}
