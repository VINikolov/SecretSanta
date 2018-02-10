using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Dapper;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace DataAccess.Implementation
{
    public class InvitationsRepository : IInvitationsRepository
    {
        public Task<IEnumerable<GroupInvitation>> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Task<GroupInvitation> SelectById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(GroupInvitation entity)
        {
            const string sql = @"INSERT INTO Invitations (Id, GroupName, Date, InvitedUser) 
                                    VALUES (@Id, @GroupName, @Date, @InvitedUser)";

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

        public Task Update(GroupInvitation entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            const string sql = @"DELETE FROM Invitations WHERE Id = @Id";
            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.Execute(sql, new { Id = id }));
            }
        }

        public Task<IEnumerable<GroupInvitation>> GetPagedInvites(string username, int skip, int take, string order)
        {
            const string sql = "SELECT GroupName, Date, Admin FROM Invitations i " +
                               "JOIN [Group] g on i.GroupName = g.Name WHERE InvitedUser = @username";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();

                var results = connection.Query<GroupInvitation>(sql, new { username = username }).Skip(skip).Take(take);
                return Task.FromResult(order == "ASC" ? results.OrderBy(x => x.Date).AsEnumerable() : results.OrderByDescending(x => x.Date).AsEnumerable());
            }
        }

        public Task<GroupInvitation> SelectByParams(string username, string groupName)
        {
            const string sql = "SELECT * FROM Invitations WHERE InvitedUser = @username AND GroupName = @groupName";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();

                return Task.FromResult(connection.QueryFirstOrDefault<GroupInvitation>(sql, new { username = username, groupName = groupName }));
            }
        }
    }
}
