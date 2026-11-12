using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Loggers; // Добавили подключение логгеров

namespace WebApplication1.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WebApplication1.Models.Category Category { get; set; } = default!;

        public void OnGet()
        {
            // Логируем открытие страницы
            InfoLogger.Instance.Log("Пользователь открыл страницу создания категории.");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Логируем предупреждение
                WarningLogger.Instance.Log("Попытка создать категорию с неверными данными.");
                return Page();
            }

            try
            {
                _context.Categories.Add(Category);
                await _context.SaveChangesAsync();

                // Логируем успех
                InfoLogger.Instance.Log($"Категория '{Category.Name}' успешно создана.");

                // В вашем исходном коде было RedirectToPage("/Index")
                // Если хотите вернуться к списку категорий, лучше "/Category/Index"
                return RedirectToPage("/Category/Index");
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                ErrorLogger.Instance.Log($"Ошибка при сохранении категории: {ex.Message}");
                return Page();
            }
        }
    }
}