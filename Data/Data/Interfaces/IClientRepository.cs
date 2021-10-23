using Entity;

namespace Data.Interfaces
{
    public interface IClientRepository
    {
        Client GetByApiUser(string apiUser);
    }
}
