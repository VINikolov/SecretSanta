using System;
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
            throw new NotImplementedException();
        }
    }
}
