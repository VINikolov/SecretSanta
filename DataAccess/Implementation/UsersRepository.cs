using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Models;

namespace DataAccess.Implementation
{
    public class UsersRepository : IUsersRepository
    {
        public Task<IEnumerable<User>> SelectAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> SelectById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
