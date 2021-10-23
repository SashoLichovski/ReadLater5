using Entity;
using Microsoft.AspNetCore.Mvc;
using ReadLater5.Models;
using Services.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    public class CategoriesController : Controller
    {
        ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CategoryViewModel();
            model.Categories = await _categoryService.GetAllAsync();
            model.Bookmark = new Bookmark();
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Category category = await _categoryService.GetByIdAsync((int)id);
            if (category == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(category);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.InsertAsync(category);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<HttpResponseMessage> Test()
        {
            var apiUser = "TestUser2";
            var apiKey = "TestKey2";
            var authToken = Encoding.ASCII.GetBytes($"{apiUser}:{apiKey}");
            var url = "https://localhost:44326/api/ApiCategory";

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/ApiCategory");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
            
            var response = await client.GetAsync(url);
            client.Dispose();
            return response;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var content = await Test();
            //if (id == null)
            //{
            //    return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            //}
            //Category category = await _categoryService.GetByIdAsync((int)id);
            //if (category == null)
            //{
            //    return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            //}
            //return View(category);
            return Ok(content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Category category = await _categoryService.GetByIdAsync((int)id);
            if (category == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Category category = await _categoryService.GetByIdAsync(id);
            try
            {
                await _categoryService.DeleteAsync(category);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Can't delete category. There are bookmarks related" });
            }
            return RedirectToAction("Index");
        }
    }
}
