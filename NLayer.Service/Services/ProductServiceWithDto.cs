using AutoMapper;
using NLayer.Data.Dto;
using NLayer.Data.Entity;
using NLayer.Data.Repository;
using NLayer.Data.Service;
using NLayer.Data.UnitOfWork;

namespace NLayer.Service.Services
{
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto>, IProductServiceWithDto
    {
        public readonly IProductRepository _productRepository;
        public ProductServiceWithDto(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork, mapper)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var returnDto = _mapper.Map<ProductDto>(entity);
            return CustomResponseDto<ProductDto>.Success(200, returnDto);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var product = await _productRepository.GetProductsWithCategory();
            await _unitOfWork.CommitAsync();
            var returnDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, returnDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
