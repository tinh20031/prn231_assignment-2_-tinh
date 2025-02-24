using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using OdataBookStore.entity;

using System.Linq;

namespace OdataBookStore.Controllers.OData
{
    [Route("odata/[controller]")]
    public class PublishersODataController : ODataController
    {
        private readonly BookStoreDBContext _context;

        public PublishersODataController(BookStoreDBContext context)
        {
            _context = context;
        }

        [EnableQuery]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Publisher);
        }

        [HttpGet("{key}")]
        public IActionResult Get(int key)
        {
            var publisher = _context.Publisher.FirstOrDefault(p => p.PubId == key);
            if (publisher == null)
                return NotFound();
            return Ok(publisher);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Publisher publisher)
        {
            _context.Publisher.Add(publisher);
            _context.SaveChanges();
            return Created(publisher);
        }

        [HttpPut("{key}")]
        public IActionResult Put(int key, [FromBody] Publisher publisher)
        {
            var existing = _context.Publisher.FirstOrDefault(p => p.PubId == key);
            if (existing == null)
                return NotFound();

            existing.PubName = publisher.PubName;
            existing.City = publisher.City;
            existing.State = publisher.State;
            existing.Country = publisher.Country;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(int key)
        {
            var publisher = _context.Publisher.FirstOrDefault(p => p.PubId == key);
            if (publisher == null)
                return NotFound();

            _context.Publisher.Remove(publisher);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
