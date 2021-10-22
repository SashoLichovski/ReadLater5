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

        public BookmarkService(IBookmarkRepository bookmarkRepository)
        {
            this.bookmarkRepository = bookmarkRepository;
        }

        public Task<int> CreateAndInsert(Bookmark bookmark)
        {
            bookmark.CreateDate = DateTime.Now;
            return bookmarkRepository.InsertAsync(bookmark);
        }

        public Task DeleteAsync(Bookmark entity)
        {
            return bookmarkRepository.DeleteAsync(entity);
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
