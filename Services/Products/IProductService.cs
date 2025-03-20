﻿using App.Repositories.Products;

namespace App.Services.Products
{
  public interface IProductService
  {
     Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
     Task<ServiceResult<List<ProductDto>>> GetAllListAsync();
     Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize);
     Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
     Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
     Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
     Task<ServiceResult> DeleteAsync(int id);
  }
}
