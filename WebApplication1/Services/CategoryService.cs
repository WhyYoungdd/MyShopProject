using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync() => await _repository.GetAllAsync();

        public async Task<Category?> GetCategoryByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task CreateCategoryAsync(Category category)
        {
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _repository.UpdateAsync(category);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
        }
    }
}