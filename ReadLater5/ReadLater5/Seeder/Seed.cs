using Entity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Seeder
{
    public class Seed
    {
        private readonly ICategoryService categoryService;
        private readonly IBookmarkService bookmarkService;
        private readonly IUserService userService;

        public Seed(ICategoryService categoryService, IBookmarkService bookmarkService, IUserService userService)
        {
            this.categoryService = categoryService;
            this.bookmarkService = bookmarkService;
            this.userService = userService;
        }

        public async Task SeedDB()
        {
            await AddUser();
            await AddCategoriesAndBookmarks();
        }

        private async Task AddCategoriesAndBookmarks()
        {
            var random = new Random();
            var categories = new List<string> { "Facebook", "Wikipedia", "Google", "Netflix", "YouTube", "Pinterest", "Instagram", "Stack Overflow", "Tweeter", "GitHub" };
            var users = await userService.GetAll();
            var ids = users.Select(x => x.Id).ToList();
            var urls = new List<string> { "https://www.youtube.com/watch?v=pswZmHSILMY", "https://www.youtube.com/watch?v=c7xO5WyErFQ", "https://www.youtube.com/watch?v=KMS7XIhCNmQ", "https://www.youtube.com/watch?v=6hI4ZFHOkI8", "https://www.youtube.com/watch?v=H2_I-tvIjNs", "https://www.youtube.com/watch?v=hPVnnd-4OtA", "https://www.youtube.com/watch?v=ux6oLtSA8es", "https://www.youtube.com/watch?v=m6xLRioPQ1Y", "https://www.youtube.com/watch?v=fH607QG9cV0", "https://www.youtube.com/watch?v=zO2i-kMIfc4" };
            foreach (var item in categories)
            {
                var model = new Category
                {
                    Name = item,
                    UserId = ids[random.Next(ids.Count)],
                    Bookmarks = new List<Bookmark>()
                };

                for (int i = 0; i < 50; i++)
                {
                    var bookmark = new Bookmark
                    {
                        UserId = ids[random.Next(ids.Count)],
                        CreateDate = DateTime.Now,
                        ShortDescription = $"Some random string",
                        TimesOpened = random.Next(2, 2000),
                        TimesAddedToFavorites = random.Next(0, 500),
                        LastTimeOpened = DateTime.Now.AddDays(-(random.Next(1, 14))),
                        URL = urls[random.Next(urls.Count)]
                    };
                    model.Bookmarks.Add(bookmark);
                }

                await categoryService.InsertAsync(model);
            }
        }

        private async Task AddUser()
        {
            var users = await userService.GetAll();
            if (!users.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    var user = new User
                    {
                        FirstName = $"testUser{i}",
                        LastName = $"testUser{i}",
                        UserName = $"testUser{i}",
                        PasswordHash = $"testUser?123"
                    };
                    await userService.RegisterUserAsync(user);
                }
            }
        }
    }
}
