using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class FavouriteBookmarks
    {
        [Key]
        public int ID { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int BookmarkId { get; set; }
        public Bookmark Bookmark { get; set; }
    }
}
