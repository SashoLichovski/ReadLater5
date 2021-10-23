using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadLater5.Mappers;
using ReadLater5.Models;
using Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Entity;
using System.Collections.Generic;
using System;

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
            var model = await bookmarkService.GetBookmarksForCategoryAsync(id);
            return View(model);
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
            var entity = await bookmarkService.GetByIdAsync(bookmark.ID);
            if (entity != null)
            {
                await bookmarkService.UpdateAndSave(bookmark.URL, bookmark.ShortDescription, entity);
            }
            return RedirectToAction("Index", new { id = entity.CategoryId });
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
            try
            {
                if (model.CategoryId == 0 && !string.IsNullOrEmpty(model.NewCategoryName))
                {
                    await categoryService.ValidateAndCreate(new Category { Name = model.NewCategoryName, Bookmarks = new List<Bookmark> { new Bookmark { URL = model.URL, ShortDescription = model.ShortDescription } } });
                    return RedirectToAction("Index", "Categories");
                }
                else
                {
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
