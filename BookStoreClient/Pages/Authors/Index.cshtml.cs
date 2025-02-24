using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using OdataBookStore.entity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreClient.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public List<Author> Authors { get; set; } = new();

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task OnGetAsync()
        {
            Authors = await _httpClient.GetFromJsonAsync<List<Author>>("https://localhost:7159/odata/Authors");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7159/odata/Authors/{id}");

            return RedirectToPage();
        }

    }
}
