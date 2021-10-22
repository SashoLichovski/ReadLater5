using Entity;
using System.Collections.Generic;

namespace ReadLater5.Models
{
    public class CategoryModel
    {
        public List<Category> Categories { get; set; }
        public Bookmark Bookmark { get; set; }
    }
}
