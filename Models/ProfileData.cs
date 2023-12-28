using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MyWebsiteApi.Models
{
    public class ProfileData
    {
        [JsonPropertyName("experiences")]
        public List<Experience>? Experiences { get; set; }
        [JsonPropertyName("projects")]
        public List<Project>? Projects { get; set; }
        [JsonPropertyName("credentials")]
        public List<Credential>? Credentials { get; set; }
    }

    public class Credential
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("institute")]
        public string? Institute { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
        [JsonPropertyName("date")]
        public string? Date { get; set; }
    }

    public class Project
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }
        [JsonPropertyName("imageUrl")]
        public string? ImageUrl { get; set; }
    }

    public class Experience
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("company")]
        public string? Location { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
        [JsonPropertyName("date")]
        public string? Date { get; set; }
    }

    public class Skill
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("tools")]
        public List<string>? Tools { get; set; }
    }
}