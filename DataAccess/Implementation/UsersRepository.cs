using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
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
            const string sql = @"INSERT INTO [User] (Id, Username, Displayname, PasswordHash, AuthenticationToken) 
                                    VALUES (@Id, @Username, @Displayname, @PasswordHash, @AuthenticationToken)";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.Execute(sql, entity));
            }
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
