using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineShop.ProductApi.Data;
using OnlineShop.ProductApi.Data.Entities;
using OnlineShop.ProductApi.Models.DTOs;
using OnlineShop.ProductApi.Services.Interfaces;

namespace OnlineShop.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext dbContext;
        private readonly IValidator<ProductDto> validator;
        private readonly IMapper mapper;

        public ProductService(
            ProductDbContext dbContext,
            IValidator<ProductDto> validator,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<Result<ProductEntity>> Create(ProductDto productDto)
        {
            var validationResult = await validator.ValidateAsync(productDto);

            if (validationResult.IsValid)
            {
                var product = mapper.Map<ProductEntity>(productDto);
                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();

                return new Result<ProductEntity>(product);
            }

            return new Result<ProductEntity>(new ValidationException(validationResult.Errors.First().ErrorMessage));
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductId.ToString() == id.ToString());

            if (product is null)
            {
                return new Result<bool>(new Exception($"Cannot find product with id: {id}"));
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return new Result<bool>(true);
        }

        [OutputCache]
        public async Task<Result<IEnumerable<ProductEntity>>> GetAll()
        {
            var products = await dbContext.Products.ToListAsync();
            return new Result<IEnumerable<ProductEntity>>(products);
        }

        [OutputCache]
        public async Task<Result<ProductEntity>> GetById(Guid id)
        {
            var product = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductId.ToString() == id.ToString());

            if (product is null)
                return new Result<ProductEntity>(new Exception($"Cannot find product with id: {id}"));

            return product;

        }

        public async Task<Result<ProductEntity>> Update(ProductDto product, Guid id)
        {
            var validationResult = validator.Validate(product);
            var productResult = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (productResult is null)
            {
                return new Result<ProductEntity>(new Exception($"Cannot find product with id: {id}"));
            }
            if (!validationResult.IsValid)
            {
                return new Result<ProductEntity>(new ValidationException(validationResult.Errors.First().ErrorMessage));
            }
            else
            {
                productResult.ProductId = id;
                productResult.ProductName = product.ProductName;
                productResult.ProductDescription = product.ProductDescription;
                productResult.Price = product.Price;
                productResult.Unit = product.Unit;

                dbContext.Products.Update(productResult);
                await dbContext.SaveChangesAsync();

                return new Result<ProductEntity>(productResult);
            }
        }
    }
}
