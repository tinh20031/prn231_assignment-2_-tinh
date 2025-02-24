using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdataBookStore.entity;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStoreClient.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public Publisher Publisher { get; set; } = new();

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7159/odata/PublishersOData", Publisher);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("./Index");

            ModelState.AddModelError("", "Thêm thất bại!");
            return Page();
        }
    }
}
