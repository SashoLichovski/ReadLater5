using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ReadLaterDataContext context;
        private DbSet<T> table = null;

        public BaseRepository(ReadLaterDataContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public Task DeleteAsync(T obj)
        {
            table.Remove(obj);
            return context.SaveChangesAsync();
        }

        public Task<List<T>> GetAllAsync()
        {
            return table.ToListAsync();
        }

        public Task<int> InsertAsync(T obj)
        {
            table.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task UpdateAsync(T obj)
        {
            context.Update<T>(obj);
            return context.SaveChangesAsync();
        }
    }
}
