﻿using Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBookmarkService
    {
        Task<int> CreateAndInsert(Bookmark bookmark);
        Task<List<Bookmark>> GetBookmarksForCategoryAsync(int categoryId);
        Task<Bookmark> GetByIdAsync(int id);
        Task UpdateAndSave(string url, string description, Bookmark bookmark);
        Task DeleteAsync(Bookmark entity);
    }
}
