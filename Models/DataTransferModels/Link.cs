using System;

namespace Models.DataTransferModels
{
    public class Link
    {
        public Guid Id { get; set; }
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public string GroupName { get; set; }
    }
}
