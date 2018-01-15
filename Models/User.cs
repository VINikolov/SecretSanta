using System;

namespace Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Displayname { get; set; }
        public string PasswordHash { get; set; }
        public string AuthenticationToken { get; set; }
    }
}
