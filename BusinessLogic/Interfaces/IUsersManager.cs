﻿using System.Threading.Tasks;
using Models.DataTransferModels;

namespace BusinessLogic.Interfaces
{
    public interface IUsersManager
    {
        Task CreateUser(User user);
    }
}
