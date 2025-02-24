using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdataBookStore.entity;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookStoreClient.Pages.Publishers
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        [BindProperty]
        public Publisher Publisher { get; set; } = new();

        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var publisher = await _httpClient.GetFromJsonAsync<Publisher>($"https://localhost:7159/odata/PublishersOData/{id}");
            if (publisher == null)
                return NotFound();

            Publisher = publisher;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7159/odata/PublishersOData/{Publisher.PubId}", Publisher);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("Index");

            ModelState.AddModelError("", "Cập nhật thất bại!");
            return Page();
        }
    }
}
