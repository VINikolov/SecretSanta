﻿using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }

        public Task Insert(Group entity)
        {
            const string sql = @"INSERT INTO [Group] (Name, Admin) 
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
            throw new System.NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}