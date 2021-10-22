using System.ComponentModel.DataAnnotations;

namespace ReadLater5.Models
{
    public class BookmarkModel
    {
        public int ID { get; set; }
        public string CategoryId { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string ShortDescription { get; set; }
    }
}
