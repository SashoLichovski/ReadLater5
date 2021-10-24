using Microsoft.AspNetCore.Mvc;
using ReadLater5.ApiModels;
using ReadLater5.Attributes;
using ReadLater5.Mappers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public ApiCategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.ReadAccess)]
        public async Task<List<ApiCategoryModel>> Get()
        {
            var entities = await categoryService.GetAllWithBookmarksAsync();
            if (entities.Any())
            {
                var model = entities.Select(x => Mapper.CategoryToApiModel(x)).ToList();
                return model;
            }
            return new List<ApiCategoryModel>();
        }


        [HttpGet("{id}")]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.ReadAccess)]
        public async Task<ApiCategoryModel> Get(int id)
        {
            var entity = await categoryService.GetByIdAsync(id, true);
            if (entity != null)
            {
                var model = Mapper.CategoryToApiModel(entity);
                return model;
            }
            return new ApiCategoryModel();
        }

        [HttpPost]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.FullAccess)]
        public async Task<IActionResult> Post([FromBody] ApiCategoryModel model)
        {
            if (model != null)
            {
                try
                {
                    await categoryService.ValidateAndCreate(Mapper.ApiModelToCategory(model));
                    return Ok(new { Message = "Category was successfully created" });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ErrorMessage = ex.Message});
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.FullAccess)]
        public async Task<IActionResult> Put(int id, [FromBody] ApiCategoryModel model)
        {
            var entity = await categoryService.GetByIdAsync(id);
            if (entity != null)
            {
                try
                {
                    entity.Name = model.Name;
                    await categoryService.UpdateAsync(entity);
                    return Ok(new { Message = "Category was successfully updated" });
                }
                catch (Exception)
                {
                    //log error
                    return BadRequest(new { ErrorMessage = "Something went wrong. Please contact support" });
                }
            }
            return BadRequest(new { ErrorMessage = "Category not found" });
        }

        [HttpDelete("{id}")]
        [ApiAuthorizationFilter(Entity.Enums.ClientAccess.FullAccess)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await categoryService.GetByIdAsync(id);
            if (entity != null)
            {
                try
                {
                    await categoryService.DeleteAsync(entity);
                    return Ok(new { Message = "Category was successfully deleted" });
                }
                catch (Exception)
                {
                    //log error
                    return BadRequest(new { ErrorMessage = "Can't delete category. There are bookmarks related" });
                }
            }
            return BadRequest(new { ErrorMessage = "Category not found" });
        }
    }
}
