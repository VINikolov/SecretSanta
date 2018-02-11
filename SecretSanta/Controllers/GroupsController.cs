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
    public class GroupsController : ApiController
    {
        private readonly IGroupsManager _groupsManager;
        private readonly IParticipantsManager _participantsManager;
        private User _currentUser;

        public GroupsController(IGroupsManager groupsManager, IParticipantsManager participantsManager)
        {
            _groupsManager = groupsManager;
            _participantsManager = participantsManager;
        }

        [HttpPost]
        [Route("api/groups")]
        public async Task<HttpResponseMessage> CreateGroup(Group group)
        {
            group.Admin = _currentUser.Username;
            await _groupsManager.CreateGroup(group);
            var groupResponse = Mapper.Map<GroupCreationResponse>(group);
            var jsonResponse = JsonConvert.SerializeObject(groupResponse);

            return new HttpResponseMessage(HttpStatusCode.Created) { Content = new StringContent(jsonResponse) };
        }

        [HttpPost]
        [Route("api/groups/{groupname}/participants")]
        public async Task<HttpResponseMessage> AcceptInvite([FromUri]string groupName, [FromBody]Participant participant)
        {
            participant.GroupName = groupName;
            await _participantsManager.AcceptInvite(participant);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpGet]
        [Route("api/groups/{groupname}/participants")]
        public async Task<HttpResponseMessage> GetParticipantsInGroup(string groupName)
        {
            var participants = await _participantsManager.GetParticipants(groupName, _currentUser.Username);
            var participantResponses = Mapper.Map<List<ParticipantResponse>>(participants);
            var jsonResponse = JsonConvert.SerializeObject(participantResponses);

            return new HttpResponseMessage { Content = new StringContent(jsonResponse) };
        }

        [HttpDelete]
        [Route("api/groups/{groupname}/participants/{participantname}")]
        public async Task<HttpResponseMessage> RemoveUserFromGroup(string groupName, string participantName)
        {
            var participantToRemove = new Participant
            {
                GroupName = groupName,
                ParticipantName = participantName
            };

            await _participantsManager.RemoveParticipant(participantToRemove, _currentUser.Username);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
    }
}
