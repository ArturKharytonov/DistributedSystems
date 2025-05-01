using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnnouncementApp.Models
{
    public class Comment
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("content")]
        [Required]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("authorName")]
        [Required]
        public string AuthorName { get; set; } = string.Empty;

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
} 