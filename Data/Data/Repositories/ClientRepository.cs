using Data.Interfaces;
using Entity;
using System.Linq;

namespace Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ReadLaterDataContext context;

        public ClientRepository(ReadLaterDataContext context)
        {
            this.context = context;
        }

        public Client GetByApiUser(string apiUser)
        {
            return context.Clients.FirstOrDefault(x => x.ApiUser == apiUser);
        }
    }
}
