using Entity;
using System.Collections.Generic;

namespace ReadLater5.ApiModels
{
    public class ApiCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ApiBookmarkModel> Bookmarks { get; set; }
    }
}
