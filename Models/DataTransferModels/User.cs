﻿using System;

namespace Models.DataTransferModels
{
    public class User
    {
        public string Username { get; set; }
        public string Displayname { get; set; }
        public string PasswordHash { get; set; }
    }
}
