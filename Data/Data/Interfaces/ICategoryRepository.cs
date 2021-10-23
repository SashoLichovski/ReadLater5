using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetByIdAsync(int id, bool includeBookmark = false);
        Task<List<Category>> GetAllWithBookmarksAsync();
    }
}
