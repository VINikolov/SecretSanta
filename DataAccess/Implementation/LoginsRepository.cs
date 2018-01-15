using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace DataAccess.Implementation
{
    public class LoginsRepository : ILoginsRepository
    {
        public Task<IEnumerable<UserLogin>> SelectAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<UserLogin> SelectById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(UserLogin entity)
        {
            const string sql = "INSERT INTO ActiveTokens(Id, Username, AuthenticationToken) VALUES(@Id, @Username, @AuthenticationToken)";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.Execute(sql, entity));
            }
        }

        public Task Update(UserLogin entity)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
