using Microsoft.AspNetCore.Mvc;
using AnnouncementApp.Models;
using AnnouncementApp.Utils;
using AnnouncementApp.ViewModels;
using Newtonsoft.Json;

namespace AnnouncementApp.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private static readonly List<Category> Categories = SetList();

        private static List<Category> SetList()
            => JsonUtil.LoadList() != string.Empty
                ? JsonConvert.DeserializeObject<List<Category>>(JsonUtil.LoadList())
                : new List<Category>();

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return Ok(Categories);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById(string id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public ActionResult<Category> Create(CreateCategoryViewModel viewModel)
        {
            var category = new Category(viewModel.Name, viewModel.Description);

            Categories.Add(category);
            JsonUtil.SaveList(Categories);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdateCategoryViewModel viewModel)
        {
            var existingCategory = Categories.FirstOrDefault(c => c.Id == id);
            if (existingCategory == null)
                return NotFound();

            existingCategory.Name = viewModel.Name;
            existingCategory.Description = viewModel.Description;

            JsonUtil.SaveList(Categories);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return NotFound();

            Categories.Remove(category);
            JsonUtil.SaveList(Categories);
            return NoContent();
        }
    }
} 