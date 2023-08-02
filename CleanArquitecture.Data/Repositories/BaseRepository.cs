using CleanArchitecture.Application.Contracts.Persistence;
using CleanArquitecture.Domain.Common;
using CleanArquitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArquitecture.Infrastructure.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly StreamerDbContext _context;

        public BaseRepository(StreamerDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> predicate = null, 
                                                    Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy = null,
                                                    string includeString = null,   
                                                    bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if(string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if(predicate!=null) query = query.Where(predicate);

            if(orderBy!=null)
                return  orderBy(query).ToList();

            return await query.ToListAsync();
        }

     

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<T> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyCollection<T>> IAsyncRepository<T>.GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedEnumerable<T>> orderBy, List<Expression<Func<T, object>>> includes, bool disableTracking)
        {
            throw new NotImplementedException();
        }

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);

        }

        public void UpdateEntity(T entity)
        {
           _context.Set<T>().Attach(entity);
           _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
