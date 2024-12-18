using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Data.Dto;
using NLayer.Data.Entity;
using NLayer.Data.Repository;
using NLayer.Data.Service;
using NLayer.Data.UnitOfWork;
using NLayer.Service.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Cache
{
    public class ProductServiceCache : IProductService
    {
        public const string CacheProductKey = "productCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductServiceCache(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;

            if(!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
            }
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            //return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey).Any(expression));
            return null;
        }

        public async Task DeleteAsync(Product entity)
        {
            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Product> entities)
        {
            _repository.DeleteRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllAsync();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

            if(product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name} not found id: {id}");
            }

            return Task.FromResult(product);
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey);
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productDto));
        }

        public Task<List<ProductWithCategoryDto>> GetProductsWithCategoryWeb()
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey);
            var productDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return Task.FromResult(productDto);
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        private async Task CacheAllAsync()
        {
            await _memoryCache.Set(CacheProductKey, _repository.GetAll().ToListAsync());
        }
    }
}
