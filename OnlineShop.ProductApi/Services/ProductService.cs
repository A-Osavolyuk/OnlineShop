using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
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
        private readonly IDistributedCache cache;
        private readonly IValidator<ProductDto> validator;
        private readonly IMapper mapper;

        public ProductService(
            ProductDbContext dbContext,
            IDistributedCache cache,
            IValidator<ProductDto> validator,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.cache = cache;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task<Result<ProductEntity>> Create(ProductDto productDto)
        {
            var validationResult = await validator.ValidateAsync(productDto);

            if (validationResult.IsValid)
            {
                await dbContext.Products.AddAsync(mapper.Map<ProductEntity>(mapper));
                await dbContext.SaveChangesAsync();

                var product = await dbContext.Products.LastAsync();

                await cache.SetStringAsync($"product-id-{product.ProductId}", JsonConvert.SerializeObject(product),
                    new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10) });

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
            await cache.RemoveAsync($"product-id-{id}");

            return new Result<bool>(true);
        }

        public async Task<Result<IEnumerable<ProductEntity>>> GetAll()
        {
            var serializedObject = await cache.GetStringAsync("products");
            var products = new List<ProductEntity>();

            if (!serializedObject.IsNullOrEmpty())
            {
                products = JsonConvert.DeserializeObject<IEnumerable<ProductEntity>>(serializedObject).ToList();
                return products;
            }
            else
            {
                products = await dbContext.Products.ToListAsync();

                if (products is null)
                    return new Result<IEnumerable<ProductEntity>>(new Exception("Cannot get list of products."));

                await cache.SetStringAsync("products", JsonConvert.SerializeObject(products),
                    new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(10) });

                return new Result<IEnumerable<ProductEntity>>(products);
            }
        }

        public async Task<Result<ProductEntity>> GetById(Guid id)
        {
            var product = JsonConvert.DeserializeObject<ProductEntity>(await cache.GetStringAsync($"product-id-{id}"));

            if (product is null)
            {
                product = dbContext.Products.FirstOrDefault(x => x.ProductId.ToString() == id.ToString());

                if (product is null)
                    return new Result<ProductEntity>(new Exception($"Cannot find product with id: {id}"));

                return product;
            }

            return product;
        }

        public async Task<Result<ProductEntity>> Update(ProductDto product, Guid id)
        {
            var validationResult = validator.Validate(product);
            var doesProductExist = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (doesProductExist is null)
            {
                return new Result<ProductEntity>(new Exception($"Cannot find product with id: {id}"));
            }
            if (!validationResult.IsValid)
            {
                return new Result<ProductEntity>(new ValidationException(validationResult.Errors.First().ErrorMessage));
            }
            else
            {
                var productEntity = mapper.Map<ProductEntity>(product);

                dbContext.Products.Update(productEntity);
                await dbContext.SaveChangesAsync();

                await cache.SetStringAsync($"product-id-{id}", JsonConvert.SerializeObject(product),
                    new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(10) });

                return new Result<ProductEntity>(productEntity);
            }
        }
    }
}
