using Entity;
using Microsoft.AspNetCore.Mvc;
using ReadLater5.ApiModels;
using ReadLater5.Attributes;
using ReadLater5.Mappers;
using ReadLater5.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBookmarkController : ControllerBase
    {
        private readonly IBookmarkService bookmarkService;

        public ApiBookmarkController(IBookmarkService bookmarkService)
        {
            this.bookmarkService = bookmarkService;
        }


        [HttpPost, Route("create")]
        public async Task<IActionResult> CreateBookmark(BookmarkModel data)
        {
            if (ModelState.IsValid)
            {
                await bookmarkService.ValidateAndCreate(Mapper.ModelToBookmark(data));
            }
            return Ok(data);
        }

        [HttpGet]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.ReadAccess)]
        public async Task<List<ApiBookmarkModel>> Get()
        {
            var entities = await bookmarkService.GetAllAsync();
            if (entities.Any())
            {
                var model = entities.Select(x => Mapper.BookmarkToApiModel(x)).ToList();
                return model;
            }
            return new List<ApiBookmarkModel>();
        }


        [HttpGet("{id}")]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.ReadAccess)]
        public async Task<ApiBookmarkModel> Get(int id)
        {
            var entity = await bookmarkService.GetByIdAsync(id);
            if (entity != null)
            {
                var model = Mapper.BookmarkToApiModel(entity);
                return model;
            }
            return new ApiBookmarkModel();
        }

        [HttpPost]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.FullAccess)]
        public async Task<IActionResult> Post([FromBody] BookmarkModel model)
        {
            if (model != null)
            {
                try
                {
                    await bookmarkService.ValidateAndCreate(Mapper.ModelToBookmark(model));
                    return Ok(new { Message = "Bookmark was successfully created" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ErrorMessage = ex.Message });
                }
            }
            return BadRequest(model);
        }

        [HttpPut("{id}")]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.FullAccess)]
        public async Task<IActionResult> Put(int id, [FromBody] BookmarkModel model)
        {
            var entity = await bookmarkService.GetByIdAsync(id);
            if (entity != null)
            {
                try
                {
                    await bookmarkService.UpdateAndSave(model.URL, model.ShortDescription, entity);
                    return Ok(new { Message = "Bookmark was successfully updated" });
                }
                catch (Exception)
                {
                    //log error
                    return BadRequest(new { ErrorMessage = "Something went wrong. Please contact support" });
                }
            }
            return BadRequest(new { ErrorMessage = "Bookmark not found" });
        }

        [HttpDelete("{id}")]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.FullAccess)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await bookmarkService.GetByIdAsync(id);
            if (entity != null)
            {
                try
                {
                    await bookmarkService.DeleteAsync(entity);
                    return Ok(new { Message = "Bookmark was successfully deleted" });
                }
                catch (Exception)
                {
                    //log error
                    return BadRequest(new { ErrorMessage = "Something went wrong. Please contact support" });
                }
            }
            return BadRequest(new { ErrorMessage = "Bookmark not found" });
        }
    }
}
