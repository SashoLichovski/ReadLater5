using Entity.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string ApiUser { get; set; }
        public string ApiKey { get; set; }
        public ClientAccess Access { get; set; }

    }
}
