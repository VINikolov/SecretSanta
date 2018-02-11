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

namespace SecretSanta.Controllers
{
    public class InvitationsController : ApiController
    {
        private readonly IInvitationsManager _invitationsManager;
        private User _currentUser;

        public InvitationsController(IInvitationsManager incInvitationsManager)
        {
            _invitationsManager = incInvitationsManager;
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

        [HttpDelete]
        [Route("api/groups/invitations/{id}")]
        public async Task<HttpResponseMessage> RejectInvite(Guid id)
        {
            await _invitationsManager.RejectInvite(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public void SetCurrentUser(User currentUser)
        {
            _currentUser = currentUser;
        }
    }
}
