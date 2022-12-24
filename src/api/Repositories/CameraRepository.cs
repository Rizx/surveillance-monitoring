using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using API.Settings;
using API.Models;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;

namespace API.Repositories
{
    public class CameraRepository
    {
        private readonly string _connectionStrings;

        public CameraRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value.Connection;
        }

        public async Task<IEnumerable<string>> GetCaptureUrl(string TYPE)
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionStrings);
            return await connection.QueryAsync<string>(
                "SELECT fotourl FROM cameras where TYPE = :TYPE ORDER BY ID DESC",
                new { TYPE });
        }

        public async Task<IEnumerable<VideoUrlResponse>> GetVideos()
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionStrings);
            return await connection.QueryAsync<VideoUrlResponse>(
                "SELECT name, videourl FROM cameras ORDER BY NAME");
        }

        public async Task<IEnumerable<Camera>> GetList()
        {
            using IDbConnection connection = new NpgsqlConnection(_connectionStrings);
            return await connection.QueryAsync<Camera>(
                "SELECT * FROM cameras ORDER BY ID DESC");
        }
    }
}
