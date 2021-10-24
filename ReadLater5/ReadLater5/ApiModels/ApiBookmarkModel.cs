using System;

namespace ReadLater5.ApiModels
{
    public class ApiBookmarkModel
    {
        public int ID { get; set; }

        public string URL { get; set; }

        public string ShortDescription { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
