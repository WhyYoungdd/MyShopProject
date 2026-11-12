using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Loggers; // 1. Добавили подключение логгеров

namespace WebApplication1.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WebApplication1.Models.Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                // 2. Логируем, если категория не найдена
                WarningLogger.Instance.Log($"Попытка редактирования несуществующей категории с Id={id}.");
                return NotFound();
            }

            Category = category;
            // 3. Логируем открытие страницы
            InfoLogger.Instance.Log($"Пользователь открыл редактирование категории '{category.Name}' (Id={id}).");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // 4. Логируем предупреждение
                WarningLogger.Instance.Log("Попытка сохранить категорию с неверными данными.");
                return Page();
            }

            _context.Attach(Category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                // 5. Логируем успех
                InfoLogger.Instance.Log($"Категория '{Category.Name}' обновлена.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // 6. Логируем ошибку
                ErrorLogger.Instance.Log($"Ошибка обновления категории: {ex.Message}");
                if (!await _context.Categories.AnyAsync(c => c.Id == Category.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToPage("/Category/Index");
        }
    }
}