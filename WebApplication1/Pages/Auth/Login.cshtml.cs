using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _authService.LoginAsync(Email, Password);

            if (result.IsSuccess)
            {
                // --- СОХРАНЕНИЕ ДАННЫХ В СЕССИЮ ---
                HttpContext.Session.SetString("UserEmail", result.User!.Name);
                HttpContext.Session.SetInt32("UserId", result.User.Id);

                return RedirectToPage("/Index");
            }

            ErrorMessage = result.ErrorMessage;
            return Page();
        }
    }
}