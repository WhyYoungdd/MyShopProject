using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthService _authService;

        public RegisterModel(IAuthService authService)
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

            // Вызов бизнес-логики (Сервиса)
            var result = await _authService.RegisterAsync(Email, Password);

            if (result.IsSuccess)
            {
                // Если успешно, отправляем на страницу входа
                return RedirectToPage("/Auth/Login");
            }

            // Если ошибка (неверный формат, занят email и т.д.)
            ErrorMessage = result.ErrorMessage;
            return Page();
        }
    }
}