using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Project.APIs.Errors;
using Store.Project.Core.Dtos.Products;
using Store.Project.Core.Helper;
using Store.Project.Core.Services.Contract;
using Store.Project.Core.Specifications.Products;

namespace Store.Project.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
       
        [HttpGet] // Get BaseUrl/api/Products
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec) // EndPoint
        {
         var result = await _productService.GetAllProductsAsync(productSpec);
            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands() // EndPoint
        {
            var result = await _productService.GetAllBrandsAsync();
            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes() // EndPoint
        {
            var result = await _productService.GetAllTypesAsync();
            return Ok(result);
        }

        [ProducesResponseType(typeof(TypeBrandDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400));
          var result = await  _productService.GetProductById(id.Value);

            if (result is null) return NotFound(new ApiErrorResponse(404, $"The Product with  Id :  {id} Not Found "));
            return Ok(result);
        }
    }
}
