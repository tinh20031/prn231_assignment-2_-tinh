using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using OdataBookStore.entity;

namespace OdataBookStore.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreDBContext _context;

        public AuthorsController(BookStoreDBContext bookStoreDBContext)
        {
            _context = bookStoreDBContext;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Author);
        }
        [HttpPost]
        [EnableQuery]
        public IActionResult Post([FromBody] Author author)
        {
            _context.Author.Add(author);
            _context.SaveChanges();
            return Ok();


        }

        [HttpPut("{id}")]
        [EnableQuery]
        public IActionResult Put( int id, [FromBody] Author author )
        {

            var exitstingAuthor = _context.Author.Find(id);
            if (exitstingAuthor == null)
            {
                return NotFound();

            }
            exitstingAuthor.AuthorId = id;
            exitstingAuthor.LastName = author.LastName;
            exitstingAuthor.FirstName = author.FirstName;
            exitstingAuthor.Phone = author.Phone;
            exitstingAuthor.Address= author.Address;
            exitstingAuthor.City = author.City;
            exitstingAuthor.State = author.State;   
            exitstingAuthor.Zip = author.Zip;
            exitstingAuthor.EmailAddress = author.EmailAddress;
            _context.SaveChanges();
            return Ok();


        }

        [HttpDelete("{key}")]  // 👈 THÊM DẤU NGOẶC TRÒN

        [EnableQuery]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int key)
        {
            var author = await _context.Author.FindAsync(key);
            if (author == null)
            {
                return NotFound();  // ⚠️ Nếu ID không tồn tại
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [EnableQuery]
        [HttpGet("{key}")]
        public IActionResult Get(int key)
        {
            var author = _context.Author.Find(key);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
    }
}
