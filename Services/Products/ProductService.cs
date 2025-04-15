using System.Net;
using App.Repositories;
using App.Repositories.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductsAsync(count);
            var productAsDto = mapper.Map<List<ProductDto>>(products);


            return new ServiceResult<List<ProductDto>>()
            {
                Data = productAsDto,
            };
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllListAsync()
        {
            var products = await productRepository.GetAll().ToListAsync();

            var productAsDto = mapper.Map<List<ProductDto>>(products);



            #region Manuel mapping
            //var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            #endregion  
            return ServiceResult<List<ProductDto>>.Success(productAsDto);

        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            // 1-10 => ilk 10 kayıt skip(0).take(10)
            // 11-20 => ikinci 10 kayıt skip(10).take(10)

            var products = await productRepository.GetAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var productAsDto = mapper.Map<List<ProductDto>>(products);

            #region Manuel Mapping

            //var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            #endregion
            return ServiceResult<List<ProductDto>>.Success(productAsDto);
        }

        public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
              return ServiceResult<ProductDto?>.Failure("Product not found", HttpStatusCode.NotFound);
            }

            var productAsDto = mapper.Map<ProductDto>(product);

            #region Manuel Mapping

            //var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

            #endregion

            return ServiceResult<ProductDto>.Success(productAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {

            var anyProduct = await productRepository.Where(x => x.Name == request.Name).AnyAsync();
            if (anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Failure("Product already exists", HttpStatusCode.BadRequest);
            }

            var product = mapper.Map<Product>(request);

            await productRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id), $"api/products/{product.Id}" );
        }
           
        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult.Failure("Product not found", HttpStatusCode.NotFound);
            }

            var isProductNameExist = await productRepository.Where(x => x.Name == request.Name && x.Id != product.Id).AnyAsync();

            if (isProductNameExist)
            {
                return ServiceResult.Failure("Product name already exists", HttpStatusCode.BadRequest);
            }

            product = mapper.Map(request, product);

            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product = await productRepository.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                return ServiceResult.Failure("Product not found", HttpStatusCode.NotFound);
            }
            product.Stock = request.Quantity;
            productRepository.Update(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult.Failure("Product not found", HttpStatusCode.NotFound);
            }
            productRepository.Delete(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

    }
}