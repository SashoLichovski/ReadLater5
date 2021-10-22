using Data.Interfaces;
using Entity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ReadLaterDataContext context;

        public CategoryRepository(ReadLaterDataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.ID == id);
        }

    }
}
