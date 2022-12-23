using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class UserRequest
    {
        [JsonPropertyName("userid")]
        public long UserId { set; get; }

        [JsonPropertyName("username")]
        public string UserName { set; get; }

        [JsonPropertyName("fullname")]
        public string FullName { set; get; }

        [JsonPropertyName("role")]
        public string Role { set; get; }

        [JsonPropertyName("password")]
        public string Password { set; get; }

        [JsonPropertyName("active")]
        public bool Active { set; get; }
    }
}