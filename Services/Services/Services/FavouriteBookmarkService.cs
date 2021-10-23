using Data.Interfaces;
using Entity;
using Services.Interfaces;
using System.Threading.Tasks;

namespace Services.Services
{
    public class FavouriteBookmarkService : IFavouriteBookmarkService
    {
        private readonly IFavouriteBookmarkRepository favouriteBookmarkRepository;

        public FavouriteBookmarkService(IFavouriteBookmarkRepository favouriteBookmarkRepository)
        {
            this.favouriteBookmarkRepository = favouriteBookmarkRepository;
        }
        public Task AddToFavouritesAsync(string userId, int bookmarkId)
        {
            var entity = new FavouriteBookmarks
            {
                BookmarkId = bookmarkId,
                UserId = userId
            };
            return favouriteBookmarkRepository.InsertAsync(entity);
        }

        public Task<FavouriteBookmarks> GetFavouriteBookmarkAsync(string userId, int bookmarkId)
        {
            return favouriteBookmarkRepository.GetFavouriteBookmarkAsync(userId, bookmarkId);
        }

        public async Task RemoveFromFavouritesAsync(FavouriteBookmarks favouriteBookmark)
        {
            await favouriteBookmarkRepository.DeleteAsync(favouriteBookmark);
        }
    }
}
