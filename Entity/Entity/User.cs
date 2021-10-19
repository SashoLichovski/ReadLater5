using Microsoft.AspNetCore.Identity;

namespace Entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
