using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLogic.Interfaces;
using Models.ApiResponseModels;
using Models.DataTransferModels;
using Newtonsoft.Json;

namespace SecretSanta.Controllers
{
    public class LinksController : ApiController
    {
        private readonly ILinksManager _linksManager;
        private User _currentUser;

        public LinksController(ILinksManager linksManager)
        {
            _linksManager = linksManager;
        }

        [HttpPost]
        [Route("api/groups/{groupname}/links")]
        public async Task<HttpResponseMessage> StartLinkingProcess(string groupName)
        {
            await _linksManager.StartLinkingProcess(groupName, _currentUser.Username);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpGet]
        [Route("api/users/{username}/groups/{groupname}/links")]
        public async Task<HttpResponseMessage> GetReceiver(string username, string groupName)
        {
            var link = await _linksManager.GetReceiver(username, groupName);
            var linkResponse = Mapper.Map<LinkResponse>(link);
            var jsonResponse = JsonConvert.SerializeObject(linkResponse);

            return new HttpResponseMessage { Content = new StringContent(jsonResponse) };
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
    }
}
