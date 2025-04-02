using App.Services.Products;

namespace App.Services.Categories
{
    public record CategoryWithProductsDto(int id, string Name, List<ProductDto> Products);

}
