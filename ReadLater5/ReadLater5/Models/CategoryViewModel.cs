using Entity;
using System.Collections.Generic;

namespace ReadLater5.Models
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; }
        public Bookmark Bookmark { get; set; }
    }
}
