using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ProductApi.Data.Entities;
using OnlineShop.ProductApi.Models.DTOs;
using OnlineShop.ProductApi.Services.Interfaces;

namespace OnlineShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(
            IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<IEnumerable<ProductEntity>>> GetAllProducts()
        {
            var result = await productService.GetAll();

            return result.Match<ActionResult<IEnumerable<ProductEntity>>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Result = succ }),
                fail => BadRequest(new ResponseDto() { IsSucceeded = false, Message = fail.Message }));
        }

        [HttpGet("{id:Guid}")]
        public async ValueTask<ActionResult<IEnumerable<ProductEntity>>> GetProductById(Guid id)
        {
            var result = await productService.GetById(id);

            return result.Match<ActionResult<IEnumerable<ProductEntity>>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Result = succ }),
                fail => NotFound(new ResponseDto() { IsSucceeded = false, Message = fail.Message }));
        }

        [HttpDelete("{id:guid}")]
        public async ValueTask<ActionResult<IEnumerable<ProductEntity>>> DeleteProductById(Guid id)
        {
            var result = await productService.Delete(id);

            return result.Match<ActionResult<IEnumerable<ProductEntity>>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Message = $"Product with id: {id} was successful deleted." }),
                fail => NotFound(new ResponseDto() { IsSucceeded = false, Message = fail.Message }));
        }

        [HttpPost]
        public async ValueTask<ActionResult<IEnumerable<ProductEntity>>> CreateProduct([FromBody] ProductDto product)
        {
            var result = await productService.Create(product);

            return result.Match<ActionResult<IEnumerable<ProductEntity>>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Result = succ }),
                fail =>
                {
                    if (fail is ValidationException exception)
                        return NotFound(new ResponseDto() { IsSucceeded = false, Message = exception.Message });
                    return NotFound(new ResponseDto() { IsSucceeded = false, Message = fail.Message });
                });
        }

        [HttpPut("{id:guid}")]
        public async ValueTask<ActionResult<IEnumerable<ProductEntity>>> UpdateProduct([FromBody] ProductDto product, Guid id)
        {
            var result = await productService.Update(product, id);

            return result.Match<ActionResult<IEnumerable<ProductEntity>>>(
                succ => Ok(new ResponseDto() { IsSucceeded = true, Result = succ }),
                fail =>
                {
                    if (fail is ValidationException exception)
                        return NotFound(new ResponseDto() { IsSucceeded = false, Message = exception.Message });
                    return NotFound(new ResponseDto() { IsSucceeded = false, Message = fail.Message });
                });
        }
    }
}
