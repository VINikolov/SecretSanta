using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Interfaces;
using Models.DataTransferModels;
using Newtonsoft.Json.Linq;

namespace SecretSanta.Controllers
{
    public class GroupsController : ApiController
    {
        private readonly IGroupsManager _groupsManager;
        private User _currentUser;

        public GroupsController(IGroupsManager groupsManager)
        {
            _groupsManager = groupsManager;
        }

        [HttpPost]
        [Route("api/groups")]
        public async Task<HttpResponseMessage> CreateGroup(Group group)
        {
            group.Admin = _currentUser.Username;
            await _groupsManager.CreateGroup(group);

            var jsonString = "{groupName : '" + group.GroupName + "', adminName: '" + _currentUser.Username + "'}";
            var json = JObject.Parse(jsonString).ToString();

            return new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(json) };
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
    }
}
