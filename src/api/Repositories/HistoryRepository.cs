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
    public class HistoryRepository
    {
        private readonly string _connectionStrings;
        private readonly EntityContext _context;

        public HistoryRepository(
            EntityContext context,
            IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value.Connection;
            _context = context;
        }

        public async Task<History> Get(long id)
        {
            return await _context.Histories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<History>> GetList()
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionStrings);
            return await connection.QueryAsync<History>(
                "SELECT * FROM histories ORDER BY ID DESC");
            // return await _context.Histories.ToListAsync();
        }

        public async Task<int> Add(History entity)
        {
            await _context.Histories.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(History entity)
        {
            _context.Histories.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
