using NLayer.Data.Dto;
using NLayer.Data.Entity;
using System.Linq.Expressions;

namespace NLayer.Data.Service
{
    public interface IServiceWithDto<TEntity, TDto> where TEntity : BaseEntity where TDto : class
    {
        Task<CustomResponseDto<TDto>> GetByIdAsync(int id);
        Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync();
        Task<CustomResponseDto<TDto>> AddAsync(TDto dto);
        Task<CustomResponseDto<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> dtoList);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(TDto dto);
        Task<CustomResponseDto<NoContentDto>> DeleteAsync(int id);
        Task<CustomResponseDto<NoContentDto>> DeleteRangeAsync(IEnumerable<int> idList);
        Task<CustomResponseDto<IQueryable<TDto>>> Where(Expression<Func<TDto, bool>> expression);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<TDto, bool>> expression);
    }
}
