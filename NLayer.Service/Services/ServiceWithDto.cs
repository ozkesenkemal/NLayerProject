using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayer.Data.Dto;
using NLayer.Data.Entity;
using NLayer.Data.Repository;
using NLayer.Data.Service;
using NLayer.Data.UnitOfWork;
using NLayer.Service.Exception;
using System.Linq.Expressions;

namespace NLayer.Service.Services
{
    public class ServiceWithDto<TEntity, TDto> : IServiceWithDto<TEntity, TDto> where TEntity : BaseEntity where TDto : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ServiceWithDto(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<TDto>> AddAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var returnDto = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(200, returnDto);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> dtoList)
        {
            IEnumerable<TEntity> entityList = _mapper.Map<IEnumerable<TEntity>>(dtoList);
            await _repository.AddRangeAsync(entityList);
            await _unitOfWork.CommitAsync();
            var returnDto = _mapper.Map<IEnumerable<TDto>>(entityList);
            return CustomResponseDto<IEnumerable<TDto>>.Success(200, returnDto);
        }

        public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            var any = await _repository.AnyAsync(expression);
            return CustomResponseDto<bool>.Success(200, any);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteRangeAsync(IEnumerable<int> idList)
        {
            var entityList = await _repository.Where(x => idList.Contains(x.Id)).ToListAsync();
            _repository.DeleteRange(entityList);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var entityList = await _repository.GetAll().ToListAsync();
            var returnDto = _mapper.Map<IEnumerable<TDto>>(entityList);
            return CustomResponseDto<IEnumerable<TDto>>.Success(200, returnDto);
        }

        public async Task<CustomResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"{typeof(TDto).Name} not found. Id: {id}");
            }

            var returnDto = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(200, returnDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<IQueryable<TDto>>> Where(Expression<Func<TEntity, bool>> expression)
        {
            var entityList = await _repository.Where(expression).ToListAsync();
            var responseDto = _mapper.Map<IQueryable<TDto>>(entityList);
            return CustomResponseDto<IQueryable<TDto>>.Success(200, responseDto);
        }
    }
}
