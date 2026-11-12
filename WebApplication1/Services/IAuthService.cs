using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IAuthService
    {
        Task<(bool IsSuccess, string ErrorMessage)> RegisterAsync(string email, string password);
        Task<(bool IsSuccess, string ErrorMessage, User? User)> LoginAsync(string email, string password);
    }
}