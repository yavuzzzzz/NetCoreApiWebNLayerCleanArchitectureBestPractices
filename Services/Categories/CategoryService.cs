﻿using System.Net;
using App.Repositories;
using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
            if (category is null)
            {
                return ServiceResult<CategoryWithProductsDto>.Failure("Category not found!", HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);
            return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
        {
            var category = await categoryRepository.GetCategoryWithProducts().ToListAsync();

            var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);

            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();

            var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
        }

        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return ServiceResult<CategoryDto>.Failure("Category not found!", HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(categoryAsDto);

        }

        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {

            var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult<int>.Failure("Category already exists", HttpStatusCode.BadRequest);
            }

            var newCategory = mapper.Map<Category>(request);

            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.SuccessAsCreated(newCategory.Id,$"api/categories/{newCategory.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return ServiceResult.Failure("Category not found!", HttpStatusCode.NotFound);
            }

            var isCategoryNameExist = await categoryRepository.Where(x => x.Name == request.Name && x.Id != category.Id)
                .AnyAsync();

            if (isCategoryNameExist)
            {
                return ServiceResult.Failure("Category name exists in Database", HttpStatusCode.BadRequest);
            }

            category = mapper.Map(request, category);

            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                return ServiceResult.Failure("Category not found!", HttpStatusCode.NotFound);
            }

            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }


    }
}

