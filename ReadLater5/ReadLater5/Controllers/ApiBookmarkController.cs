using Entity;
using Microsoft.AspNetCore.Mvc;
using ReadLater5.Mappers;
using ReadLater5.Models;
using Services.Interfaces;
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
                var result = await bookmarkService.CreateAndInsert(Mapper.ModelToBookmark(data));
            }

            return Ok(data);
        }
    }
}
