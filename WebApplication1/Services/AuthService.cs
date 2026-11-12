using System.Text.RegularExpressions;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterAsync(string email, string password)
        {
            // 1. Валидация через Regex
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return (false, "Неверный формат email");

            if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$"))
                return (false, "Пароль должен быть от 6 символов и содержать буквы и цифры");

            // 2. Проверка, есть ли уже такой пользователь
            var existingUser = await _userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                return (false, "Пользователь с таким email уже существует");

            // 3. Хеширование пароля
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                Name = email, // Используем поле Name для email
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow
            };

            // 4. Сохранение через репозиторий
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();

            return (true, "Успешная регистрация");
        }

        public async Task<(bool IsSuccess, string ErrorMessage, User? User)> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return (false, "Пользователь не найден", null);

            // Проверка пароля (сравниваем введенный пароль с хешем в БД)
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return (false, "Неверный пароль", null);

            return (true, "", user);
        }
    }
}