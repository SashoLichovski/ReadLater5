using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> InsertAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(T obj);
        Task<List<T>> GetAllAsync();
    }
}
