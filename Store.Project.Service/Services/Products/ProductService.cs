using AutoMapper;
using Store.Project.Core;
using Store.Project.Core.Dtos.Products;
using Store.Project.Core.Entities;
using Store.Project.Core.Helper;
using Store.Project.Core.Services.Contract;
using Store.Project.Core.Specifications;
using Store.Project.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper  )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec)
        {
            var spec = new ProductSpecifications(productSpec);
            var product = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
            var mappedProduct = _mapper.Map<IEnumerable<ProductDto>>(product);

            var CountSpec = new ProductWithCountSpecifications(productSpec);

            var count = await _unitOfWork.Repository<Product, int>().GetCountAsync(spec);
            return new PaginationResponse<ProductDto>(productSpec.PageSize, productSpec.PageIndex, count, mappedProduct);
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
           return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }

       

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductType, int>().GetAllAsync());
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            var product = await _unitOfWork.Repository<Product , int >().GetWithSpecAsync(spec);
            var mappedProduct = _mapper.Map<ProductDto>(product);
            return mappedProduct;
        }
    }
}
