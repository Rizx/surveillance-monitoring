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
    public class UserRepository
    {
        private readonly string _connectionStrings;
        private readonly EntityContext _context;

        public UserRepository(
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
                "SELECT * FROM users WHERE USERNAME = :USERNAME ORDER BY ID DESC",
                new { USERNAME });
        }

        public async Task<User> Get(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetList()
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionStrings);
            return await connection.QueryAsync<User>(
                "SELECT * FROM users ORDER BY FULLNAME");
        }

        public async Task<int> Add(User entity)
        {
            await _context.Users.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(User entity)
        {
            _context.Users.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
