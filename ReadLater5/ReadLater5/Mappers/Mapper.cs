using Entity;
using ReadLater5.Models;
using System;

namespace ReadLater5.Mappers
{
    public static class Mapper
    {
        public static User RegisterModelToUser(RegisterModel m)
        {
            return new User()
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                UserName = m.Username,
                PasswordHash = m.Password
            };
        }

        public static Bookmark ModelToBookmark(BookmarkModel b)
        {
            return new Bookmark
            {
                URL = b.URL,
                CategoryId = Convert.ToInt32(b.CategoryId),
                ShortDescription = b.ShortDescription
            };
        }

        internal static BookmarkModel BookmarkToModel(Bookmark m)
        {
            return new BookmarkModel
            {
                ID = m.ID,
                CategoryId = m.CategoryId.ToString(),
                ShortDescription = m.ShortDescription,
                URL = m.URL
            };
        }
    }
}
