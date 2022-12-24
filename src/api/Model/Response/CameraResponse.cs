using System.Text.Json.Serialization;

namespace API.Models
{
    public record CameraResponse
    {
        [JsonPropertyName("name")]
        public string Name { set; get; }

        [JsonPropertyName("model")]
        public string Model { set; get; }

        [JsonPropertyName("username")]
        public string UserName { set; get; }

        [JsonPropertyName("password")]
        public string Password { set; get; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { set; get; }
    }
}