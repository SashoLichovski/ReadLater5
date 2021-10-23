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
    }
}
