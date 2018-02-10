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

        public ParticipantsManager(IParticipantsRepository participantsRepository, IInvitationsRepository invitationsRepository)
        {
            _participantsRepository = participantsRepository;
            _invitationsRepository = invitationsRepository;
        }

        public async Task AcceptInvite(Participant participant)
        {
            var invitation = await _invitationsRepository.SelectByParams(participant.Name, participant.GroupName);
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
    }
}
