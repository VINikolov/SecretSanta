using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace DataAccess.Implementation
{
    public class LinksRepository : ILinksRepository
    {
        public Task<IEnumerable<Link>> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Task<Link> SelectById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Link entity)
        {
            const string sql = "INSERT INTO Links(Id, Receiver, Sender, GroupName)" +
                               "VALUES(@Id, @Receiver, @Sender, @GroupName)";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.Execute(sql, entity));
            }
        }

        public Task Update(Link entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Link> SelectByParams(string username, string groupName)
        {
            const string sql = "SELECT * FROM Links WHERE Sender = @username AND GroupName = @groupName";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();

                return Task.FromResult(connection.QueryFirstOrDefault<Link>(sql, new { username = username, groupName = groupName }));
            }
        }
    }
}
