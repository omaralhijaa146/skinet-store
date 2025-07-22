using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skinet.API.Dtos;
using skinet.API.Errors;
using skinet.API.Helpers;
using skinet.Core.Entities;
using skinet.Core.Interfaces;
using skinet.Core.Specifications;
using skinet.Infrastructure.Data;


namespace skinet.API.Controllers
{
    
    public class ProductsController : BaseApiController
    {

        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> productBrandRepository,IGenericRepository<ProductType> productTypeRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _productBrandRepository = productBrandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<Product>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec= new ProductWithFiltersForCountSpecifications(productParams);

            var totalItems = await _productRepository.CountAsync(countSpec);
            
            var products = await _productRepository.ListAllAsyncSpec(spec);
            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            return Ok(new Pagination<ProductDto>(productParams.PageIndex,productParams.PageSize,totalItems,data));
        }
        
        [HttpGet("{id}")]
        
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productRepository.GetEntityWithSpec(spec);

            if (product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            
            return _mapper.Map<ProductDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var result = await _productBrandRepository.ListAllAsync();
            
            return Ok(result);
        }
        
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            var result = await _productTypeRepository.ListAllAsync();
            
            return Ok(result);
        }
        
        
        
    }
}
