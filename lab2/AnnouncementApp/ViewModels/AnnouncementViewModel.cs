using System.ComponentModel.DataAnnotations;

namespace AnnouncementApp.ViewModels
{
    public class AnnouncementViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime AddingDate { get; set; }

        public string? CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public int CommentsCount { get; set; }
    }

    public class CreateAnnouncementViewModel
    {
        [Required] [StringLength(200)] public string Title { get; set; } = string.Empty;

        [Required] [StringLength(2000)] public string Description { get; set; } = string.Empty;

    }

    public class UpdateAnnouncementViewModel
    {
        [Required] [StringLength(200)] public string Title { get; set; } = string.Empty;

        [Required] [StringLength(2000)] public string Description { get; set; } = string.Empty;
    }
}
        
 