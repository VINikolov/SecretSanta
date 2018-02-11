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
        private readonly IParticipantsManager _participantsManager;

        public UsersController(IUsersManager usersManager, IParticipantsManager participantsManager)
        {
            _usersManager = usersManager;
            _participantsManager = participantsManager;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateUser(User user)
        {
            await _usersManager.CreateUser(user);
            var userRegResponse = Mapper.Map<UserRegistrationResponse>(user);
            var jsonResponse = JsonConvert.SerializeObject(userRegResponse);

            var response =
                new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(jsonResponse) };
            return response;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetPagedUsers(int skip, int take, string order, string searchPhrase = null)
        {
            var users = await _usersManager.GetPagedUsers(skip, take, order, searchPhrase);
            var usersResponseModels = Mapper.Map<List<UserResponse>>(users);
            var jsonResponse = JsonConvert.SerializeObject(usersResponseModels);

            var response = new HttpResponseMessage { Content = new StringContent(jsonResponse) };
            return response;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetUser(string id)
        {
            var user = await _usersManager.GetUserByUsername(id);
            var userResponseModel = Mapper.Map<UserResponse>(user);
            var jsonResponse = JsonConvert.SerializeObject(userResponseModel);

            var response = new HttpResponseMessage { Content = new StringContent(jsonResponse) };
            return response;
        }

        [HttpGet]
        [Route("api/Users/{username}/groups/{skip}/{take}")]
        public async Task<HttpResponseMessage> GetGroupsForUser(string username, int skip, int take)
        {
            var groups = await _participantsManager.GetGroupsForUser(username, skip, take);
            var groupResponseModels = Mapper.Map<List<GroupResponse>>(groups);
            var jsonResponse = JsonConvert.SerializeObject(groupResponseModels);

            return new HttpResponseMessage { Content = new StringContent(jsonResponse) };
        }
    }
}
