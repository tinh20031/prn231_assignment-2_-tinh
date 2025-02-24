using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using OdataBookStore.entity;

namespace BookStoreClient.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private const string API_URL = "https://localhost:7159/odata/Books";

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<Book> BookList { get; set; } = new List<Book>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(API_URL);
                BookList = JsonConvert.DeserializeObject<List<Book>>(response) ?? new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy dữ liệu: {ex.Message}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(Book book)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(API_URL, jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateAsync(Book book)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{API_URL}/{book.BookId}", jsonContent);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{API_URL}/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            return Page();
        }
    }
}
