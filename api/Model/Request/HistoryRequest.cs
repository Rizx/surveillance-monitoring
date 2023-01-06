using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class HistoryRequest
    {
        [JsonPropertyName("activity")]
        public string Activity { set; get; }

        [JsonPropertyName("cardid")]
        public string CardId { set; get; }

        [JsonPropertyName("state")]
        public string State { set; get; }
    }
}