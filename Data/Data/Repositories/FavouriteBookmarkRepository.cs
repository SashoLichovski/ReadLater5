using Data.Interfaces;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FavouriteBookmarkRepository : BaseRepository<FavouriteBookmarks>, IFavouriteBookmarkRepository
    {
        private readonly ReadLaterDataContext context;

        public FavouriteBookmarkRepository(ReadLaterDataContext context) : base(context)
        {
            this.context = context;
        }

        public Task<FavouriteBookmarks> GetFavouriteBookmarkAsync(string userId, int bookmarkId)
        {
            return context.FavouriteBookmarks.FirstOrDefaultAsync(x => x.UserId == userId && x.BookmarkId == bookmarkId);
        }
    }
}
