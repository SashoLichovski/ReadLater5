using Data.Interfaces;
using Entity;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public Task DeleteAsync(Category category)
        {
            return categoryRepository.DeleteAsync(category);
        }

        public Task<List<Category>> GetAllAsync()
        {
            return categoryRepository.GetAllAsync();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return categoryRepository.GetByIdAsync(id);
        }

        public Task InsertAsync(Category categ)
        {
            return categoryRepository.InsertAsync(categ);
        }

        public Task UpdateAsync(Category category)
        {
            return categoryRepository.UpdateAsync(category);
        }
    }
}
