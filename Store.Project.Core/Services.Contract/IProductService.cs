using Store.Project.Core.Dtos.Products;
using Store.Project.Core.Helper;
using Store.Project.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Core.Services.Contract
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec);

        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();

        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();

        Task<ProductDto> GetProductById(int id);
    }
}
