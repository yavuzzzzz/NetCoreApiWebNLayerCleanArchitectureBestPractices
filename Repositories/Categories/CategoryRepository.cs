using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories
{
    public class CategoryRepository(AppDbContext appDbContext) : GenericRepository<Category>(appDbContext), ICategoryRepository
    {
        public Task<Category?> GetCategoryWithProductsAsync(int id)
        {
            return appDbContext.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);

        }

        public IQueryable<Category> GetCategoriesByProductsAsync()
        {
            return appDbContext.Categories.Include(x => x.Products).AsQueryable();
        }
    }
}
