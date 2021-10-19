using Data;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ReadLaterDataContext context;
        public CategoryService(ReadLaterDataContext readLaterDataContext)
        {
            this.context = readLaterDataContext;
        }

        public Category CreateCategory(Category category)
        {
            context.Add(category);
            context.SaveChanges();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            context.Update(category);
            context.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category GetCategory(int Id)
        {
            return context.Categories.Where(c => c.ID == Id).FirstOrDefault();
        }

        public Category GetCategory(string Name)
        {
            return context.Categories.Where(c => c.Name == Name).FirstOrDefault();
        }

        public void DeleteCategory(Category category)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }
}
