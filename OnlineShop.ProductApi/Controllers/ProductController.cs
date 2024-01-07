using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.ProductApi.Data.Entities;
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
                succ => Ok(succ), fail => BadRequest(fail.Message));
        }
    }
}
