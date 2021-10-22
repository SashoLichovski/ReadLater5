using Data.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BookmarkRepository : BaseRepository<Bookmark>, IBookmarkRepository
    {
        private readonly ReadLaterDataContext context;

        public BookmarkRepository(ReadLaterDataContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<Bookmark>> GetBookmarksForCategoryAsync(int categoryId)
        {
            return context.Bookmark.Where(x => x.CategoryId == categoryId).Include(x => x.Category).ToListAsync();
        }

        public Task<Bookmark> GetByIdAsync(int id)
        {
            return context.Bookmark.FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
