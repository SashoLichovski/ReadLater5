using Entity;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFavouriteBookmarkService
    {
        Task AddToFavouritesAsync(string userId, int id);
        Task RemoveFromFavouritesAsync(FavouriteBookmarks favouriteBookmarks);
        Task<FavouriteBookmarks> GetFavouriteBookmarkAsync(string userId, int bookmarkId);
    }
}
