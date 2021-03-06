using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Bookmark
    {
        public int ID { get; set; }

        [StringLength(maximumLength: 500)]
        public string URL { get; set; }

        public string ShortDescription { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int TimesOpened { get; set; }
        public int TimesAddedToFavorites { get; set; }
        public DateTime LastTimeOpened { get; set; }
    }
}
