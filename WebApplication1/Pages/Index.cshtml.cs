using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;
// Убрали using WebApplication1.Models, чтобы не было конфликта имен с папкой Pages/Category

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Указали полный путь: WebApplication1.Models.Category
        public List<WebApplication1.Models.Category> Categories { get; set; } = new();

        public async Task OnGetAsync()
        {
            Categories = await _categoryService.GetAllCategoriesAsync();
        }
    }
}