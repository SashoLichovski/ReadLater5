using Entity;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IFavouriteBookmarkRepository : IBaseRepository<FavouriteBookmarks>
    {
        Task<FavouriteBookmarks> GetFavouriteBookmarkAsync(string userId, int bookmarkId);
    }
}
