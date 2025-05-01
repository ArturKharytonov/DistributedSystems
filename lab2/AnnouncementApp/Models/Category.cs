using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnnouncementApp.Models
{
    public class Category
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("announcements")]
        public List<Announcement> Announcements { get; set; } = [];

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}