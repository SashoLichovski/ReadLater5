using Data.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ReadLaterDataContext context;

        public CategoryRepository(ReadLaterDataContext context) : base(context)
        {
            this.context = context;
        }

        public Task<Category> GetByIdAsync(int id, bool includeBookmark = false)
        {
            if (includeBookmark)
                return context.Categories.Include(x => x.Bookmarks).FirstOrDefaultAsync(x => x.ID == id);

            return context.Categories.FirstOrDefaultAsync(x => x.ID == id);
        }

        public Task<List<Category>> GetAllWithBookmarksAsync()
        {
            return context.Categories.Include(x => x.Bookmarks).ToListAsync();
        }
    }
}
