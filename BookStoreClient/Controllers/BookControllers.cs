using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OdataBookStore.entity;

namespace BookStoreClient.Controllers
{
    public class BookControllers : Controller
    {
        private readonly HttpClient _httpClient;    
        public BookControllers(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        [HttpGet]
        public async Task<IActionResult> GetBook()
        {
            string apiURL = "https://localhost:7159/odata/Books";
            var response = await _httpClient.GetStringAsync(apiURL);
            var books = JsonConvert.DeserializeObject<List<Book>>(response);
            return Ok(books);
        }


        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            string apiUrl = "https://localhost:7159/odata/Books";
            var response = await _httpClient.PostAsJsonAsync(apiUrl, book);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Thêm sách thành công!");
            }

            return BadRequest("Lỗi khi thêm sách");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            string apiUrl = $"https://localhost:7159/odata/Books/{id}";
            var response = await _httpClient.PutAsJsonAsync(apiUrl, book);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Cập nhật sách thành công!");
            }

            return BadRequest("Lỗi khi cập nhật sách");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            string apiUrl = $"https://localhost:7159/odata/Books/{id}";
            var response = await _httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Xóa sách thành công!");
            }

            return BadRequest("Lỗi khi xóa sách");
        }
    }
}
