using Data.Interfaces;
using Entity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository bookmarkRepository;
        private readonly ICategoryService categoryService;

        public BookmarkService(IBookmarkRepository bookmarkRepository, ICategoryService categoryService)
        {
            this.bookmarkRepository = bookmarkRepository;
            this.categoryService = categoryService;
        }

        public Task<List<Bookmark>> GetByUserIdAsync(string userId)
        {
            return bookmarkRepository.GetByUserIdAsync(userId);
        }

        public async Task<int> ValidateAndCreate(Bookmark bookmark)
        {
            if (string.IsNullOrEmpty(bookmark.URL) || string.IsNullOrEmpty(bookmark.ShortDescription))
                throw new Exception("URL and Short description required");
            
            if (await categoryService.GetByIdAsync(bookmark.CategoryId) == null)
                throw new Exception($"Category with ID {bookmark.CategoryId} not found");

            return await bookmarkRepository.InsertAsync(bookmark);
        }

        public Task DeleteAsync(Bookmark entity)
        {
            return bookmarkRepository.DeleteAsync(entity);
        }

        public Task<List<Bookmark>> GetAllAsync()
        {
            return bookmarkRepository.GetAllAsync();
        }

        public Task<List<Bookmark>> GetBookmarksForCategoryAsync(int categoryId)
        {
            return bookmarkRepository.GetBookmarksForCategoryAsync(categoryId);
        }

        public Task<Bookmark> GetByIdAsync(int id)
        {
            return bookmarkRepository.GetByIdAsync(id);
        }

        public Task UpdateAndSave(string url, string description, Bookmark bookmark)
        {
            bookmark.ShortDescription = description;
            bookmark.URL = url;
            return bookmarkRepository.UpdateAsync(bookmark);
        }

        public async Task<List<Bookmark>> GetFavouritesAsync(string userId)
        {
            List<int> favIds = await bookmarkRepository.GetFavouriteIdsAsync(userId);
            return await bookmarkRepository.GetFavouritesAsync(favIds);
        }

        public async Task UpdateTrackingStats(int id)
        {
            var bookmark = await GetByIdAsync(id);
            if (bookmark != null)
            {
                bookmark.TimesOpened++;
                bookmark.LastTimeOpened = DateTime.Now;
                await bookmarkRepository.UpdateAsync(bookmark);
            }
        }

        public async Task UpdateFavoriteStats(int id, bool isAdd)
        {
            var bookmark = await GetByIdAsync(id);
            if (bookmark != null && isAdd)
            {
                bookmark.TimesAddedToFavorites++;
                await bookmarkRepository.UpdateAsync(bookmark);
            }
            else if (bookmark != null && !isAdd)
            {
                bookmark.TimesAddedToFavorites--;
                await bookmarkRepository.UpdateAsync(bookmark);
            }
        }
    }
}
