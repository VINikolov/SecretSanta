using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Dapper;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace DataAccess.Implementation
{
    public class GroupsRepository : IGroupsRepository
    {
        public Task<IEnumerable<Group>> SelectAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Group> SelectById(string id)
        {
            const string sql = @"SELECT * FROM [Group] WHERE GroupName = @Id";
            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.QueryFirstOrDefault<Group>(sql, new { Id = id }));
            }
        }

        public Task Insert(Group entity)
        {
            const string sql = @"INSERT INTO [Group] (GroupName, Admin) 
                                    VALUES (@GroupName, @Admin)";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                try
                {
                    connection.Open();
                    return Task.FromResult(connection.Execute(sql, entity));
                }
                catch (SqlException e) when (e.Number == 2627)
                {
                    throw new HttpResponseException(HttpStatusCode.Conflict);
                }
            }
        }

        public Task Update(Group entity)
        {
            const string sql = @"UPDATE [Group] SET Admin = @Admin, 
                                    LinkingProcessDone = @LinkingProcessDone
                                    WHERE GroupName = @GroupName";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.Execute(sql, entity));
            }
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
