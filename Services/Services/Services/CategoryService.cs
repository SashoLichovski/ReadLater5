using Data.Interfaces;
using Entity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<List<Category>> GetAllWithBookmarksAsync()
        {
            return categoryRepository.GetAllWithBookmarksAsync();
        }

        public Task<Category> GetByIdAsync(int id, bool includeBookmark = false)
        {
            return categoryRepository.GetByIdAsync(id, includeBookmark);
        }

        public Task InsertAsync(Category categ)
        {
            return categoryRepository.InsertAsync(categ);
        }

        public Task UpdateAsync(Category category)
        {
            return categoryRepository.UpdateAsync(category);
        }

        public async Task ValidateAndCreate(Category category)
        {
            if (category.Bookmarks.Any() && category.Bookmarks.Any(x => x.URL == null || x.ShortDescription == null))
                throw new Exception("Bookmark URLs and ShortDescriptions are required");

            if (string.IsNullOrEmpty(category.Name))
                throw new Exception("Category name is required");

            await InsertAsync(category);
        }
    }
}
