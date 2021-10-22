using Entity;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetByIdAsync(int id);
    }
}
