using System;

namespace Models.DataTransferModels
{
    public class GroupInvitation
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string Admin { get; set; }
        public DateTime Date { get; set; }
        public string InvitedUser { get; set; }
    }
}
