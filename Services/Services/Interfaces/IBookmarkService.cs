using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookmarkService
    {
        Task<int> ValidateAndCreate(Bookmark bookmark);
        Task<List<Bookmark>> GetBookmarksForCategoryAsync(int categoryId);
        Task<Bookmark> GetByIdAsync(int id);
        Task UpdateAndSave(string url, string description, Bookmark bookmark);
        Task DeleteAsync(Bookmark entity);
        Task<List<Bookmark>> GetAllAsync();
        Task<List<Bookmark>> GetByUserIdAsync(string userId);
        Task<List<Bookmark>> GetFavouritesAsync(string userId);
    }
}
