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
    public class MemberRepository
    {
        private readonly string _connectionStrings;
        private readonly EntityContext _context;

        public MemberRepository(
            EntityContext context,
            IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value.Connection;
            _context = context;
        }

        public async Task<Member> GetByUsername(string USERNAME)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionStrings);
            return await connection.QueryFirstOrDefaultAsync<Member>(
                "SELECT * FROM members WHERE USERNAME = :USERNAME ORDER BY ID DESC",
                new { USERNAME });
        }

        public async Task<Member> Get(long id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<IEnumerable<Member>> GetList()
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionStrings);
            return await connection.QueryAsync<Member>(
                "SELECT * FROM members ORDER BY FULLNAME");
        }

        public async Task<int> Add(Member entity)
        {
            await _context.Members.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Member entity)
        {
            _context.Members.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
