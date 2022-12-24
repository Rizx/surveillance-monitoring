using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class HistoryResponse
    {
        [JsonPropertyName("userid")]
        public long Id { set; get; }

        [JsonPropertyName("activity")]
        public string Activity { set; get; }

        [JsonPropertyName("name")]
        public string Name { set; get; }

        [JsonPropertyName("address")]
        public string Address { set; get; }

        [JsonPropertyName("state")]
        public string State { set; get; }

        [JsonPropertyName("date")]
        public DateTime Date { set; get; }

        [JsonPropertyName("photos")]
        public List<string> Photos { set; get; }
    }
}