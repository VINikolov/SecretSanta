using System;

namespace Models.ApiResponseModels
{
    public class GroupInvitationInfoResponse
    {
        public string GroupName { get; set; }
        public DateTime Date { get; set; }
        public string Admin { get; set; }
    }
}
