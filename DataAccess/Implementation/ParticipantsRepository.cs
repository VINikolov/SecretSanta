using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                                    VALUES (@Id, @Name, @GroupName)";

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
    }
}
