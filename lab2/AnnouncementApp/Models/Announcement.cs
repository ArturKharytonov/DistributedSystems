using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnnouncementApp.Models
{
    public class Announcement
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        [Required]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("addingDate")]
        public DateTime AddingDate { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("comments")]
        public List<Comment> Comments { get; set; }

        public Announcement() { }

        public Announcement(string title, string description)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Description = description;
            AddingDate = DateTime.UtcNow;
            Comments = [];
        }
    }
}
