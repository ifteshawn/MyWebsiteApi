using System;
using System.Text.Json.Serialization;

namespace MyWebsiteApi.Models
{
    public class MailData
    {
        [JsonPropertyName("senderName")]
        public string? SenderName { get; set; }
        [JsonPropertyName("senderEmail")]
        public string? SenderEmail { get; set; }
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
