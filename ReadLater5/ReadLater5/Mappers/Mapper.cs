using Entity;
using ReadLater5.Models;

namespace ReadLater5.Mappers
{
    public static class Mapper
    {
        public static User RegisterModelToUser(RegisterModel m)
        {
            return new User()
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                UserName = m.Username,
                PasswordHash = m.Password
            };
        }
    }
}
