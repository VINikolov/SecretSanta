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
    public class InvitationsManager : IInvitationsManager
    {
        private readonly IInvitationsRepository _invitationsRepository;
        private readonly IGroupsRepository _groupsRepository;

        public InvitationsManager(IInvitationsRepository invitationsRepository, IGroupsRepository groupsRepository)
        {
            _invitationsRepository = invitationsRepository;
            _groupsRepository = groupsRepository;
        }

        public async Task<Guid> CreateInvitation(GroupInvitation invitation)
        {
            var group = await _groupsRepository.SelectById(invitation.GroupName);

            if (group == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (!group.Admin.Equals(invitation.Admin))
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            invitation.Id = Guid.NewGuid();
            await _invitationsRepository.Insert(invitation);

            return invitation.Id;
        }

        public async Task<IEnumerable<GroupInvitation>> GetPagedInvites(string username, int skip, int take, string order)
        {
            return await _invitationsRepository.GetPagedInvites(username, skip, take, order);
        }
    }
}
