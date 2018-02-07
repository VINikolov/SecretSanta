using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Interfaces;
using Models.DataTransferModels;
using System.Web.Http;

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
            const string sql = @"SELECT * FROM [USER] WHERE Username = @Id";
            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.QueryFirstOrDefault<User>(sql, new { Id = id }));
            }
        }

        public Task Insert(User entity)
        {
            const string sql = @"INSERT INTO [User] (Username, Displayname, PasswordHash) 
                                    VALUES (@Username, @Displayname, @PasswordHash)";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                try
                {
                    connection.Open();
                    return Task.FromResult(connection.Execute(sql, entity));
                }
                catch (SqlException e) when(e.Number == 2627)
                {
                    throw new HttpResponseException(HttpStatusCode.Conflict);
                }
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

        public Task<User> GetUserByAuthenticationToken(string authenticationToken)
        {
            const string sql =
                "SELECT * FROM [User] u JOIN ActiveTokens t ON u.Username = t.Username WHERE AuthenticationToken = @token";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.QueryFirstOrDefault<User>(sql, new {token = authenticationToken}));
            }
        }
    }
}
