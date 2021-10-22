using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBookmarkRepository : IBaseRepository<Bookmark>
    {
        Task<List<Bookmark>> GetBookmarksForCategoryAsync(int categoryId);
        Task<Bookmark> GetByIdAsync(int id);
    }
}
