using System;

namespace Models.DataTransferModels
{
    public class Participant
    {
        public Guid Id { get; set; }
        public string ParticipantName { get; set; }
        public string GroupName { get; set; }
    }
}
