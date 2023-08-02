using CleanArquitecture.Domain.Common;
using System.Linq.Expressions;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : BaseDomainModel
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();

        //Expression Function  
        Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> predicate);

        Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                              Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
                                              string includeString = null,
                                              bool disableTracking = true);

        Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                      Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
                                      List<Expression<Func<T,object>>> includes = null,
                                      bool disableTracking = true);

        Task<T> GetByIdAsync(int id);


        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);

        void AddEntity(T entity);
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);
    }
}
