using App.Repositories.Products;

namespace App.Services.Products
{
  public interface IProductService
  {
      public Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
  }
}
