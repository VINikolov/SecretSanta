using System;
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
    public class GroupsController : ApiController
    {
        private readonly IGroupsManager _groupsManager;
        private readonly IInvitationsManager _invitationsManager;
        private readonly IParticipantsManager _participantsManager;
        private User _currentUser;

        public GroupsController(IGroupsManager groupsManager, IInvitationsManager invitationsManager,
            IParticipantsManager participantsManager)
        {
            _groupsManager = groupsManager;
            _invitationsManager = invitationsManager;
            _participantsManager = participantsManager;
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

        [HttpGet]
        [Route("api/groups/{username}/{skip}/{take}/{order}")]
        public async Task<HttpResponseMessage> GetPagedInvites(string username, int skip, int take, string order)
        {
            if (!username.Equals(_currentUser.Username))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            var invites = await _invitationsManager.GetPagedInvites(username, skip, take, order);
            var invitesResponseModels = Mapper.Map<List<GroupInvitationInfoResponse>>(invites);
            var jsonResponse = JsonConvert.SerializeObject(invitesResponseModels);

            return new HttpResponseMessage { Content = new StringContent(jsonResponse) };
        }

        [HttpPost]
        [Route("api/groups/{groupname}/participants")]
        public async Task<HttpResponseMessage> AcceptInvite([FromUri]string groupName, [FromBody]Participant participant)
        {
            participant.GroupName = groupName;
            await _participantsManager.AcceptInvite(participant);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpDelete]
        [Route("api/groups/invitations/{id}")]
        public async Task<HttpResponseMessage> RejectInvite(Guid id)
        {
            await _invitationsManager.RejectInvite(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
    }
}
