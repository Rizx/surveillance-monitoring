using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class AuthenticationResponse
    {
        [JsonPropertyName("id")]
        public long Id { set; get; }

        [JsonPropertyName("username")]
        public string UserName { set; get; }

        [JsonPropertyName("fullname")]
        public string FullName { set; get; }

        [JsonPropertyName("role")]
        public string Role { set; get; }

        [JsonPropertyName("token")]
        public string Token { set; get; }
    }
}