using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class MemberResponse
    {
        [JsonPropertyName("memberid")]
        public long Id { set; get; }

        [JsonPropertyName("username")]
        public string UserName { set; get; }

        [JsonPropertyName("fullname")]
        public string FullName { set; get; }

        [JsonPropertyName("address")]
        public string Address { set; get; }

        [JsonPropertyName("cardNumber")]
        public string CardNumber { set; get; }

        [JsonPropertyName("fotoProfile")]
        public string FotoProfile { set; get; }

        [JsonPropertyName("active")]
        public bool Active { set; get; }
    }
}