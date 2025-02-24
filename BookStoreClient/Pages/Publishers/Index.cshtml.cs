using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using OdataBookStore.entity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreClient.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public List<Publisher> Publishers { get; set; } = new();

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGetAsync()
        {
            Publishers = await _httpClient.GetFromJsonAsync<List<Publisher>>("https://localhost:7113/api/PublishersApi");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7159/odata/PublishersOData/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("", "Xóa thất bại!");
            return Page();
        }
    }
}
