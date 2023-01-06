using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class MemberRequest
    {
        [JsonPropertyName("id")]
        public long Id { set; get; }

        [JsonPropertyName("username")]
        public string UserName { set; get; }
        
        [JsonPropertyName("password")]
        public string Password { set; get; }

        [JsonPropertyName("fullname")]
        public string FullName { set; get; }

        [JsonPropertyName("address")]
        public string Address { set; get; }

        [JsonPropertyName("cardid")]
        public string CardId { set; get; }

        [JsonPropertyName("fotoProfile")]
        public string FotoProfile { set; get; }

        [JsonPropertyName("active")]
        public bool Active { set; get; }
    }
}