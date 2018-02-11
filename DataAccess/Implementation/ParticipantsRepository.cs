using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Interfaces;
using Models.DataTransferModels;

namespace DataAccess.Implementation
{
    public class ParticipantsRepository : IParticipantsRepository
    {
        public Task<IEnumerable<Participant>> SelectAll()
        {
            throw new NotImplementedException();
        }

        public Task<Participant> SelectById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Participant entity)
        {
            const string sql = @"INSERT INTO GroupParticipants (Id, ParticipantName, GroupName) 
                                    VALUES (@Id, @ParticipantName, @GroupName)";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.Execute(sql, entity));
            }
        }

        public Task Update(Participant entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Participant>> SelectGroupsForUser(string username, int skip, int take)
        {
            const string sql = "SELECT * FROM GroupParticipants WHERE ParticipantName = @username";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();

                var results = connection.Query<Participant>(sql, new { username = username }).Skip(skip).Take(take);
                return Task.FromResult(results.OrderBy(x => x.GroupName).AsEnumerable());
            }
        }

        public Task<IEnumerable<Participant>> SelectByGroupName(string groupName)
        {
            const string sql = "SELECT * FROM GroupParticipants WHERE groupName = @groupName";

            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();

                return Task.FromResult(connection.Query<Participant>(sql, new { groupName = groupName }));
            }
        }

        public Task<int> DeleteByParams(Participant participantToRemove)
        {
            const string sql = @"DELETE FROM GroupParticipants WHERE ParticipantName = @name AND GroupName = @groupName";
            using (var connection = new SqlConnection(Settings.DbConnectionString))
            {
                connection.Open();
                return Task.FromResult(connection.Execute(sql, new
                {
                    name = participantToRemove.ParticipantName,
                    groupName = participantToRemove.GroupName
                }));
            }
        }
    }
}
