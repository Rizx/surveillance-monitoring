using System;
using System.Data;
using System.Threading.Tasks;
using API.Settings;
using AutogateAPI.Models;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace API.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionStrings;

        public UserRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value.Connection;
        }

        public async Task<Users> GetByUsername(string USERNAME)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionStrings);
            return await connection.QueryFirstOrDefaultAsync<Users>(
                "SELECT * FROM users WHERE USERNAME = :USERNAME ORDER BY ID DESC",
                new {USERNAME});
        }
    }
}
