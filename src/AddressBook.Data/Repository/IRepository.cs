using AddressBook.Data.Repository.Parameters;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AddressBook.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(
               Expression<Func<T, bool>> filter = null,
               TextSearchParameters textSearchParameters = null,
               SortingParameters orderingParameters = null,
               PagingParameters pagingParameters = null,
               Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
               bool disableTracking = false);

        Task<int> GetCountAsync(
            Expression<Func<T, bool>> filter = null,
            TextSearchParameters textSearchParameters = null);

        Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null, int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = false);

        Task<T> GetOneAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<T> GetFirstAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<T> GetByKeyAsync(params object[] keyValues);

        Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);

        Task AddAsync(T entity);

        void Update(T entity);

        Task UpdateChangedPropertiesOnlyAsync(T entity, params object[] keyValues);

        Task DeleteAsync(params object[] keyValues);

        void Delete(T entity);

        Task<int> SaveAsync();
    }
}
