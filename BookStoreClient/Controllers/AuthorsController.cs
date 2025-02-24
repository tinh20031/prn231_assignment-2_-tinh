using Microsoft.AspNetCore.Mvc;
using OdataBookStore.entity;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.OData.Query;

namespace BookStoreClient.Controllers
{

    public class AuthorsController : Controller
    {
        private readonly HttpClient _httpClient;

        public AuthorsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ✅ Lấy danh sách tất cả tác giả từ OData API
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllAuthors()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Author>>(
                "https://localhost:7159/odata/Authors");

            if (response == null || response.Count == 0)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // ✅ Lấy thông tin một tác giả theo ID
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<List<Author>>(
                $"https://localhost:7159/odata/Authors?$filter=AuthorId eq {id}");

            if (response == null || response.Count == 0)
            {
                return NotFound();
            }

            return Ok(response.First());
        }

        // ✅ Cập nhật thông tin tác giả
        [HttpPut("{id}")]
        [EnableQuery]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author author)
        {
            if (author == null || id != author.AuthorId)
            {
                return BadRequest();
            }

            var json = JsonContent.Create(author);
            var response = await _httpClient.PutAsync(
                $"https://localhost:7159/odata/Authors({id})", json);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Cập nhật thất bại!");
            }

            return NoContent();
        }
    }
}
