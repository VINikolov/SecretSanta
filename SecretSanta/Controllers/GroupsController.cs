﻿using System.Net;
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
    public class GroupsController : ApiController
    {
        private readonly IGroupsManager _groupsManager;
        private readonly IInvitationsManager _invitationsManager;
        private User _currentUser;
        
        public GroupsController(IGroupsManager groupsManager, IInvitationsManager invitationsManager)
        {
            _groupsManager = groupsManager;
            _invitationsManager = invitationsManager;
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

        [HttpPost]
        [Route("api/groups/{groupname}/invitations")]
        public async Task<HttpResponseMessage> SendInvitation([FromBody]GroupInvitation invitation, [FromUri] string groupName)
        {
            invitation.GroupName = groupName;
            var invitationId = await _invitationsManager.CreateInvitation(invitation);
            var invitationResponse = new GroupInvitationResponse
            {
                Id = invitationId
            };

            var jsonContent = JsonConvert.SerializeObject(invitationResponse);
            return new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(jsonContent) };
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
    }
}
