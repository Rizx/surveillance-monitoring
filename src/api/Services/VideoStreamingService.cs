using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    public class VideoStreamingService
    {
        // HttpClient _client;
        // Stream _stream;

        public async Task<Stream> GetStreamAsync(string username, string password, string videoUrl)
        {
            var handler = new HttpClientHandler { PreAuthenticate = true, Credentials = new NetworkCredential(username, password) };
            using var _client = new HttpClient(handler)
            {
                Timeout = new TimeSpan(0, 2, 0)
            };

            return await _client.GetStreamAsync(videoUrl);
        }

        // public void Dispose()
        // {
        //     _client?.Dispose();
        //     _stream?.Flush();
        //     _stream?.Dispose();
        // }
    }
}