using System.IO;
using System.Text;
using AnnouncementApp.Models;
using AnnouncementApp.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnnouncementApp.Controllers
{
    [Route("api/announcements")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private static readonly List<Announcement> Announcements = SetList();

        private static List<Announcement> SetList() 
            =>  JsonUtil.LoadList() != string.Empty 
                ? JsonConvert.DeserializeObject<List<Announcement>>(JsonUtil.LoadList()) 
                : new List<Announcement>();

        [HttpPost]
        public IActionResult AddAnnouncement(Announcement announcement)
        {
            Announcement anotherAnnouncement = new Announcement(announcement.Title, announcement.Description);

            Announcements.Add(anotherAnnouncement);
            JsonUtil.SaveList(Announcements);
            return Ok("Announcement was added!");
        }

        [HttpGet]
        public IActionResult GetAllAnnouncement()
        {
            if (Announcements.Count > 0)
                return Ok(Announcements);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnnouncement([FromRoute] string id)
        {
            if (Announcements.Count <= 0) 
                return NoContent();

            foreach (var value in Announcements)
            {
                if (value.Id == id)
                {
                    Announcements.Remove(value);
                    JsonUtil.SaveList(Announcements);
                    return Ok("Was deleted");
                }
            }

            return NotFound();
        }
        [HttpPut("{id}")]
        public IActionResult EditAnnouncement([FromRoute] string id, [FromBody] Announcement announcement)
        {
            if (Announcements.Count <= 0) 
                return NoContent();
            for (int i = 0; i < Announcements.Count; i++)
            {
                if (Announcements[i].Id == id)
                {
                    Announcements[i] = announcement;
                    Announcements[i].Id = id;
                    JsonUtil.SaveList(Announcements);
                    return Ok("Was updated");
                }
            }

            return NotFound();

        }

        [HttpGet("similar")]
        public IActionResult GetSimilarAnnouncement(string title, string description)
        {
            List<Announcement> result = new List<Announcement>();
            if (Announcements.Count <= 0) return NoContent();
            foreach (var value in Announcements)
            {
                if (result.Count == 3)
                    return Ok(result);

                string[] titleWords = title.Split(" ");
                string[] descriptionWords = description.Split(" ");

                bool contains = titleWords.Any(t => value.Title.Contains(t));

                if (contains)
                    result.AddRange(from t in descriptionWords where value.Description.Contains(t) select value);
            }

            if (result.Count > 0)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("info")]
        public IActionResult GetInfo(string id)
        {
            if (Announcements.Count <= 0) 
                return NoContent();

            foreach (var value in Announcements)
            {
                if (value.Id == id)
                    return Ok(value);
            }
            return NotFound();
        }
    }
}
