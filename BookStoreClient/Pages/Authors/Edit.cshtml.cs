using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using OdataBookStore.entity;

namespace BookStoreClient.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public Author Author { get; set; } = new();

        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ✅ Lấy dữ liệu từ API trung gian của Client (Không gọi trực tiếp OData API)
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var responseList = await _httpClient.GetFromJsonAsync<List<Author>>(
    $"https://localhost:7159/odata/Authors?$filter=AuthorId eq {id}");

            if (responseList == null || responseList.Count == 0)
            {
                return NotFound();
            }

            Author = responseList.First();
            return Page();

        }

        // ✅ Gửi dữ liệu cập nhật đến API trung gian
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var json = JsonSerializer.Serialize(Author);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(
                $"https://localhost:7159/odata/Authors/{Author.AuthorId}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Cập nhật thất bại. Vui lòng thử lại!");
            return Page();
        }
    }
}
