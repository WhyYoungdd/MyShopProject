using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<User> Users { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                for (int i = 1; i <= 5; i++)
                {
                    _context.Users.Add(new User
                    {
                        Name = $"user{i}@example.com",
                        PasswordHash = $"fake_hash_{i}"
                    });
                }
                await _context.SaveChangesAsync();
            }

            Users = await _context.Users.ToListAsync();
        }
    }
}