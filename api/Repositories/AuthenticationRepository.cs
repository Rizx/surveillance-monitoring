using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using API.Settings;
using API.Models;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AuthenticationRepository
    {
        private readonly string _connectionStrings;
        private readonly EntityContext _context;

        public AuthenticationRepository(
            EntityContext context,
            IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value.Connection;
            _context = context;
        }

        public async Task<User> GetByUsername(string USERNAME)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionStrings);
            return await connection.QueryFirstOrDefaultAsync<User>(
                @"SELECT ID, FULLNAME, USERNAME, PASSWORD, ROLE, ACTIVE FROM users 
                    WHERE USERNAME = :USERNAME 
                  UNION
                  SELECT ID, FULLNAME, USERNAME, PASSWORD, 'Guest' AS ROLE, ACTIVE FROM MEMBERS
                    WHERE USERNAME = :USERNAME",
                new { USERNAME });
        }

        public async Task<User> Get(long id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
