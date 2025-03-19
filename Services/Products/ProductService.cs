using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repositories.Products;

namespace App.Services.Products
{
    public class ProductService (IProductRepository productRepository) : IProductService
    {
    }
}