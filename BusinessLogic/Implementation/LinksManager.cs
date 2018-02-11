using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace BusinessLogic.Implementation
{
    public class LinksManager : ILinksManager
    {
        private readonly IGroupsRepository _groupsRepository;
        private readonly ILinksRepository _linksRepository;
        private readonly IParticipantsRepository _participantsRepository;

        public LinksManager(IGroupsRepository groupsRepository, ILinksRepository linksRepository, IParticipantsRepository participantsRepository)
        {
            _groupsRepository = groupsRepository;
            _linksRepository = linksRepository;
            _participantsRepository = participantsRepository;
        }

        public async Task StartLinkingProcess(string groupName, string username)
        {
            var group = await _groupsRepository.SelectById(groupName);
            EvaluateConditions(group, username);

            var participants = (await _participantsRepository.SelectByGroupName(groupName)).ToList();
            if (participants.Count() == 1)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            var linksList = LinkParticipants(participants);

            foreach (var link in linksList)
            {
                link.Id = Guid.NewGuid();
                link.GroupName = groupName;
                await _linksRepository.Insert(link);
            }

            group.LinkingProcessDone = true;
            await _groupsRepository.Update(group);
        }

        public async Task<Link> GetReceiver(string username, string groupName)
        {
            var link = await _linksRepository.SelectByParams(username, groupName);
            if (link == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return link;
        }

        private void EvaluateConditions(Group group, string username)
        {
            if (group == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (group.Admin != username)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            if (group.LinkingProcessDone)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        private IEnumerable<Link> LinkParticipants(IList<Participant> participants)
        {
            var linksList = new List<Link>();
            var participantsCopy = new List<Participant>(participants);
            var rng = new Random();

            var index = 0;
            foreach (var participant in participants)
            {
                var range = Enumerable.Range(0, participantsCopy.Count).Where(x => x != index);
                var receiverIndex = range.ElementAt(rng.Next(0, participantsCopy.Count - 1));
                
                var linkToAdd = new Link
                {
                    Receiver = participantsCopy.ElementAt(receiverIndex).ParticipantName,
                    Sender = participant.ParticipantName
                };

                participantsCopy.RemoveAt(receiverIndex);
                linksList.Add(linkToAdd);
                index++;
            }

            return linksList;
        }
    }
}
