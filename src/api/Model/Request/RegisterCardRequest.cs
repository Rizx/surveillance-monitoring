using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class RegisterCardRequest
    {
        [JsonPropertyName("cardid")]
        public string CardId { set; get; }

        [JsonPropertyName("username")]
        public string UserName { set; get; }
    }
}