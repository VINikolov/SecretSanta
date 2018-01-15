using System;

namespace Models.DataTransferModels
{
    public class UserLogin
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string AuthenticationToken { get; set; }
    }
}
