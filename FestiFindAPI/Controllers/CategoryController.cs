using FestFindV2.Models;
using FestiFindV5.Data;
using FestiFindV5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FestiFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _context.Category.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string Name, IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
            {
                // Handle the case where no file was uploaded.
                return BadRequest();
            }

            using (var memoryStream = new MemoryStream()) //Dit is nodig zodat het als byte[] kan worden opgeslagen
            {
                await Image.CopyToAsync(memoryStream);
                Category c = new Category
                {
                    Name = Name,
                    // Store the image data as a byte array
                    Image = memoryStream.ToArray()
                };
                _context.Category.Add(c);
                _context.SaveChanges();
            }

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, string Name, IFormFile Image)
        {
            Category categoryToUpdate = _context.Category.FirstOrDefault(c => c.Id == id);

            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            categoryToUpdate.Name = Name;

            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    categoryToUpdate.Image = memoryStream.ToArray();
                }
            }

            _context.Update(categoryToUpdate);
            _context.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            _context.SaveChangesAsync();

            return Ok();
        }
    }
}
