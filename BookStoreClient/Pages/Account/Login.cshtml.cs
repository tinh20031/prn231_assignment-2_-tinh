using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using OdataBookStore.entity;
using OdataBookStore;

namespace BookStoreClient.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly BookStoreDBContext _context;

        [BindProperty]
        public string Email { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public LoginModel(BookStoreDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.Users
                .FirstOrDefault(u => u.EmailAddress == Email && u.Password == Password);

            if (user == null)
            {
                ErrorMessage = "Sai email hoặc mật khẩu!";
                return Page();
            }

            // Lưu thông tin đăng nhập vào session
            HttpContext.Session.SetString("UserEmail", user.EmailAddress ?? "");
            HttpContext.Session.SetInt32("UserId", user.UserId);

            return RedirectToPage("/Index");
        }
    }
}
