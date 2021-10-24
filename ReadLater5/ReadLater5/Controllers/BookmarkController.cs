using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadLater5.Mappers;
using ReadLater5.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    [Authorize]
    public class BookmarkController : Controller
    {
        private readonly IBookmarkService bookmarkService;
        private readonly ICategoryService categoryService;

        public BookmarkController(IBookmarkService bookmarkService, ICategoryService categoryService)
        {
            this.bookmarkService = bookmarkService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var entities = await bookmarkService.GetBookmarksForCategoryAsync(id);
            ViewBag.Page = "Index";
            if (entities.Any())
            {
                return View(entities.Select(x => Mapper.BookmarkToModel(x)).ToList());
            }
            return View(new List<BookmarkModel>());
        }

        public async Task<IActionResult> FavouriteBookmarks()
        {
            ViewBag.Page = "Favorite";
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var bookmarks = await bookmarkService.GetFavouritesAsync(userId);
            return View(bookmarks.Select(x => Mapper.BookmarkToModel(x)));
        }

        public async Task<IActionResult> MyBookmarks()
        {
            ViewBag.Page = "MyBookmarks";
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Bookmark> entities = await bookmarkService.GetByUserIdAsync(userId);
            return View(entities.Select(x => Mapper.BookmarkToModel(x)).ToList());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await bookmarkService.GetByIdAsync(id);
            return View(Mapper.BookmarkToModel(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookmarkModel bookmark)
        {
            if (ModelState.IsValid)
            {
                var entity = await bookmarkService.GetByIdAsync(bookmark.ID);
                if (entity != null)
                {
                    await bookmarkService.UpdateAndSave(bookmark.URL, bookmark.ShortDescription, entity);
                }
                return RedirectToAction("Index", new { id = entity.CategoryId });
            }
            return View(bookmark);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAllAsync();
            var model = new BookmarkModel
            {
                Categories = categories.Select(x => Mapper.CategoryToModel(x)).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookmarkModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                if (model.CategoryId == 0 && !string.IsNullOrEmpty(model.NewCategoryName))
                {
                    await categoryService.ValidateAndCreate(new Category { Name = model.NewCategoryName, UserId = userId, Bookmarks = new List<Bookmark> { new Bookmark { URL = model.URL, ShortDescription = model.ShortDescription, UserId = userId, CreateDate = DateTime.Now } } });
                    return RedirectToAction("Index", "Categories");
                }
                else
                {
                    model.UserId = userId;
                    await bookmarkService.ValidateAndCreate(Mapper.ModelToBookmark(model));
                    return RedirectToAction("Index", "Categories");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = ex.Message});
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await bookmarkService.GetByIdAsync(id);
            if (entity != null)
            {
                await bookmarkService.DeleteAsync(entity);
            }
            return RedirectToAction("Index", new { id = entity.CategoryId });
        }
    }
}
