using Microsoft.AspNetCore.Mvc;
using AnnouncementApp.Models;
using AnnouncementApp.Utils;
using AnnouncementApp.ViewModels;
using Newtonsoft.Json;

namespace AnnouncementApp.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private static readonly List<Category> Categories = SetList();

        private static List<Category> SetList()
            => JsonUtil.LoadList() != string.Empty
                ? JsonConvert.DeserializeObject<List<Category>>(JsonUtil.LoadList())
                : new List<Category>();

        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetAll()
        {
            var allComments = Categories
                .SelectMany(c => c.Announcements)
                .SelectMany(a => a.Comments)
                .ToList();

            return Ok(allComments);
        }

        [HttpGet("announcement/{announcementId}")]
        public ActionResult<IEnumerable<Comment>> GetByAnnouncementId(string announcementId)
        {
            var announcement = Categories
                .SelectMany(x => x.Announcements)
                .FirstOrDefault(x => x.Id == announcementId);

            if (announcement == null)
                return NotFound($"Announcement with ID {announcementId} not found");

            return Ok(announcement.Comments);
        }

        [HttpGet("{id}")]
        public ActionResult<Comment> GetById(string id)
        {
            var (comment, _) = FindCommentAndAnnouncement(id);
            if (comment == null)
                return NotFound($"Comment with ID {id} not found");

            return Ok(comment);
        }

        [HttpPost("announcement/{announcementId}")]
        public ActionResult<CommentViewModel> Create(string announcementId, CreateCommentViewModel viewModel)
        {
            var announcement = Categories
                .SelectMany(x => x.Announcements)
                .FirstOrDefault(x => x.Id == announcementId);

            if (announcement == null)
                return NotFound($"Announcement with ID {announcementId} not found");

            var comment = new Comment
            {
                Content = viewModel.Content,
                AuthorName = viewModel.AuthorName
            };

            announcement.Comments.Add(comment);
            JsonUtil.SaveList(Categories);

            return CreatedAtAction(
                nameof(GetById), 
                new { id = comment.Id }
            );
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdateCommentViewModel viewModel)
        {
            var (comment, _) = FindCommentAndAnnouncement(id);
            if (comment == null)
                return NotFound($"Comment with ID {id} not found");

            comment.Content = viewModel.Content;
            comment.AuthorName = viewModel.AuthorName;

            JsonUtil.SaveList(Categories);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var (comment, announcement) = FindCommentAndAnnouncement(id);
            if (comment == null)
                return NotFound($"Comment with ID {id} not found");

            announcement!.Comments.Remove(comment);
            JsonUtil.SaveList(Categories);
            return NoContent();
        }

        private static (Comment? Comment, Announcement? Announcement) FindCommentAndAnnouncement(string commentId)
        {
            foreach (var category in Categories)
            {
                foreach (var announcement in category.Announcements)
                {
                    var comment = announcement.Comments.FirstOrDefault(c => c.Id == commentId);
                    if (comment != null)
                    {
                        return (comment, announcement);
                    }
                }
            }
            return (null, null);
        }
    }
} 