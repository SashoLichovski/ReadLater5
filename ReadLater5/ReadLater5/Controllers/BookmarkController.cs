using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadLater5.Mappers;
using ReadLater5.Models;
using Services.Interfaces;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    [Authorize]
    public class BookmarkController : Controller
    {
        private readonly IBookmarkService bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            this.bookmarkService = bookmarkService;
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
