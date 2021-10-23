using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task InsertAsync(Category category);
        Task DeleteAsync(Category category);
        Task UpdateAsync(Category category);
        Task<Category> GetByIdAsync(int id, bool includeBookmark = false);
        Task<List<Category>> GetAllAsync();
        Task<List<Category>> GetAllWithBookmarksAsync();
        Task ValidateAndCreate(Category category);
    }
}
