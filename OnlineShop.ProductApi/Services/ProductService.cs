using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductApi.Data;
using OnlineShop.ProductApi.Data.Entities;
using OnlineShop.ProductApi.Mapping;
using OnlineShop.ProductApi.Models.DTOs;
using OnlineShop.ProductApi.Services.Interfaces;

namespace OnlineShop.ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductDbContext dbContext;
        private readonly IValidator<ProductDto> validator;
        private readonly ProductMapper productMapper;

        public ProductService(
            ProductDbContext dbContext,
            IValidator<ProductDto> validator
        )
        {
            this.dbContext = dbContext;
            this.validator = validator;
            productMapper = new ProductMapper();

        }

        public async Task<Result<ProductEntity>> Create(ProductDto productDto)
        {
            var validationResult = await validator.ValidateAsync(productDto);

            if (validationResult.IsValid)
            {
                var product = productMapper.FromProductDtoToProductEntity(productDto);
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
                var prod = productMapper.FromProductDtoToProductEntity(product);
                prod.ProductId = id;

                dbContext.Products.Update(prod);
                await dbContext.SaveChangesAsync();

                return new Result<ProductEntity>(prod);
            }
        }
    }
}
