using Entity;
using ReadLater5.ApiModels;
using ReadLater5.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadLater5.Mappers
{
    public static class Mapper
    {
        internal static CategoryModel CategoryToModel(Category c)
        {
            return new CategoryModel
            {
                Id = c.ID,
                Name = c.Name
            };
        }

        internal static User RegisterModelToUser(RegisterModel m)
        {
            return new User()
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                UserName = m.Username,
                PasswordHash = m.Password
            };
        }

        internal static Bookmark ModelToBookmark(BookmarkModel b)
        {
            return new Bookmark
            {
                URL = b.URL,
                CategoryId = b.CategoryId,
                ShortDescription = b.ShortDescription
            };
        }

        internal static BookmarkModel BookmarkToModel(Bookmark m)
        {
            return new BookmarkModel
            {
                ID = m.ID,
                CategoryId = m.CategoryId,
                ShortDescription = m.ShortDescription,
                URL = m.URL
            };
        }

        internal static ApiCategoryModel CategoryToApiModel(Category c)
        {
            var model = new ApiCategoryModel
            {
                Id = c.ID,
                Name = c.Name,
                Bookmarks = c.Bookmarks.Any() ? c.Bookmarks.Select(x => new ApiBookmarkModel
                {
                    ID = x.ID,
                    CategoryId = x.CategoryId,
                    CreateDate = x.CreateDate,
                    ShortDescription = x.ShortDescription,
                    URL = x.URL
                }).ToList() : new List<ApiBookmarkModel>()
            };
            return model;
        }

        internal static ApiBookmarkModel BookmarkToApiModel(Bookmark x)
        {
            return new ApiBookmarkModel
            {
                ID = x.ID,
                URL = x.URL,
                ShortDescription = x.ShortDescription,
                CreateDate = x.CreateDate,
                CategoryId = x.CategoryId
            };
        }

        internal static Category ApiModelToCategory(ApiCategoryModel c)
        {
            return new Category
            {
                Name = c.Name,
                Bookmarks = c.Bookmarks.Any() ? c.Bookmarks.Select(x => new Bookmark
                {
                    URL = x.URL,
                    ShortDescription = x.ShortDescription,
                    CreateDate = DateTime.Now
                }).ToList() : new List<Bookmark>()
            };
        }
    }
}
