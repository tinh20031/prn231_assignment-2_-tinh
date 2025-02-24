using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OdataBookStore.entity;

namespace OdataBookStore.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly BookStoreDBContext _context;

        public BooksController (BookStoreDBContext bookStoreDBContext)
        {
            _context = bookStoreDBContext;
        }
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Books);

        }
        [HttpPost]
        [EnableQuery]
        public IActionResult Post([FromBody] Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges(); 
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book book)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
            {
                return NotFound($"Không tìm thấy sách có ID = {id}");
            }

            existingBook.Title = book.Title;
            existingBook.Type = book.Type;
            existingBook.Price = book.Price;
            existingBook.Advance = book.Advance;
            existingBook.Royalty= book.Royalty;
            existingBook.YtlSales = book.YtlSales;
            existingBook.Note= book.Note;


            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [EnableQuery]
        public IActionResult Delete(int id)
        {
            var deletebook = _context.Books.Find(id);   
            _context.Books.Remove(deletebook);
            _context.SaveChanges();
            return NoContent();

        }
    }

}
