using System.ComponentModel.DataAnnotations;

namespace AnnouncementApp.ViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; } = string.Empty;
        
        public string Content { get; set; } = string.Empty;
        
        public string AuthorName { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; }
        
        public string AnnouncementId { get; set; } = string.Empty;
        
        public string? AnnouncementTitle { get; set; }
    }

    public class CreateCommentViewModel
    {
        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; } = string.Empty;
    }

    public class UpdateCommentViewModel
    {
        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; } = string.Empty;
    }
} 