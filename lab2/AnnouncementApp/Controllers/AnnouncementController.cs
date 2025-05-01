using AnnouncementApp.Models;
using AnnouncementApp.Utils;
using AnnouncementApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnnouncementApp.Controllers
{
    [Route("api/announcements")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private static readonly List<Category> Categories = SetList();

        private static List<Category> SetList()
            => JsonUtil.LoadList() != string.Empty
                ? JsonConvert.DeserializeObject<List<Category>>(JsonUtil.LoadList())
                : new List<Category>();


        [HttpPost("category/{id}")]
        public IActionResult AddAnnouncement([FromRoute] string id, CreateAnnouncementViewModel announcement)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return BadRequest();
            Announcement newAnnouncement = new Announcement(announcement.Title, announcement.Description);

            category.Announcements.Add(newAnnouncement);
            JsonUtil.SaveList(Categories);
            return Ok("Announcement was added!");
        }

        [HttpGet("category/{id}")]
        public IActionResult GetAllAnnouncements([FromRoute] string id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return BadRequest();

            if (category.Announcements.Count > 0)
                return Ok(category.Announcements);

            return NoContent();
        }

        [HttpDelete("category/{categoryId}/announcement/{id}")]
        public IActionResult DeleteAnnouncement([FromRoute] string categoryId, [FromRoute] string id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return BadRequest();

            if (category.Announcements.Count <= 0) 
                return NoContent();

            foreach (var value in category.Announcements.Where(value => value.Id == id))
            {
                category.Announcements.Remove(value);
                JsonUtil.SaveList(Categories);
                return Ok("Was deleted");
            }

            return NotFound();
        }

        [HttpPut("category/{categoryId}/announcement/{id}")]
        public IActionResult EditAnnouncement([FromRoute] string categoryId, [FromRoute] string id, [FromBody] UpdateAnnouncementViewModel updatedAnnouncement)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return BadRequest();

            if (category.Announcements.Count <= 0) 
                return NoContent();

            for (int i = 0; i < category.Announcements.Count; i++)
            {
                if (category.Announcements[i].Id == id)
                {
                    Announcement announcementEntity =
                        new Announcement(updatedAnnouncement.Title, updatedAnnouncement.Description);

                    category.Announcements[i] = announcementEntity;
                    category.Announcements[i].Id = id;
                    JsonUtil.SaveList(Categories);
                    return Ok("Was updated");
                }
            }
            
            return NotFound();

        }
    }
}
