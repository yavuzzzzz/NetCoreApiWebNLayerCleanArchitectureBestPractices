using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Products
{
   public class ProductRepository(AppDbContext appDbContext) : GenericRepository<Product>(appDbContext), IProductRepository
    {
        public Task<List<Product>> GetTopPriceProductsAsync(int count)
        {
            return Context.Products.OrderByDescending(p => p.Price).Take(count).ToListAsync(); 
        }
    }
}
