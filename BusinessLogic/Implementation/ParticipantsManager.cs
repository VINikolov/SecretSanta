using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace BusinessLogic.Implementation
{
    public class ParticipantsManager : IParticipantsManager
    {
        private readonly IParticipantsRepository _participantsRepository;
        private readonly IInvitationsRepository _invitationsRepository;
        private readonly IGroupsRepository _groupsRepository;

        public ParticipantsManager(IParticipantsRepository participantsRepository,
            IInvitationsRepository invitationsRepository,
            IGroupsRepository groupsRepository)
        {
            _participantsRepository = participantsRepository;
            _invitationsRepository = invitationsRepository;
            _groupsRepository = groupsRepository;
        }

        public async Task AcceptInvite(Participant participant)
        {
            var invitation = await _invitationsRepository.SelectByParams(participant.ParticipantName, participant.GroupName);
            if (invitation == null)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            participant.Id = Guid.NewGuid();
            await _participantsRepository.Insert(participant);
            await _invitationsRepository.Delete(invitation.Id);
        }

        public async Task<IEnumerable<Participant>> GetGroupsForUser(string username, int skip, int take)
        {
            return await _participantsRepository.SelectGroupsForUser(username, skip, take);
        }

        public async Task<IEnumerable<Participant>> GetParticipants(string groupName, string username)
        {
            var group = await _groupsRepository.SelectById(groupName);
            if (!group.Admin.Equals(username))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            return await _participantsRepository.SelectByGroupName(groupName);
        }

        public async Task RemoveParticipant(Participant participantToRemove, string currentUser)
        {
            var group = await _groupsRepository.SelectById(participantToRemove.GroupName);
            if (!group.Admin.Equals(currentUser))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            var deletedRows = await _participantsRepository.DeleteByParams(participantToRemove);
            if (deletedRows == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
