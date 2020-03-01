using AddressBook.Data.Repository.Extensions;
using AddressBook.Data.Repository.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace AddressBook.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AddressBookContext context;

        public Repository(AddressBookContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            var dbSet = context.Set<T>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async Task DeleteAsync(params object[] keyValues)
        {
            T entity = await context.Set<T>().FindAsync(keyValues);
            Delete(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null, int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = false)
        {
            return await GetQueryable(null, orderBy, skip, take, include, disableTracking).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            TextSearchParameters textSearchParameters = null,
            SortingParameters sortingParameters = null,
            PagingParameters pagingParameters = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = false)
        {
            IQueryable<T> query = context.Set<T>();

            query = ProcessFilter(filter, query);

            query = ProcessDisableTracking(disableTracking, query);

            query = ProcessTextSearchParameters(textSearchParameters, query);

            query = ProcessSortingParameters(sortingParameters, query);

            query = ProcessPagingParameters(pagingParameters, query);

            query = ProcessIncludes(include, query);

            return await query.ToListAsync();
        }

        public async Task<T> GetByKeyAsync(params object[] keyValues)
        {
            return await context.Set<T>().FindAsync(keyValues);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null, TextSearchParameters textSearchParameters = null)
        {
            IQueryable<T> query = context.Set<T>();

            query = ProcessFilter(filter, query);

            query = ProcessTextSearchParameters(textSearchParameters, query);

            return await query.CountAsync();
        }

        public async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
        {
            return await GetQueryable(filter).AnyAsync();
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await GetQueryable(filter, orderBy, null, null, include).FirstOrDefaultAsync();
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await GetQueryable(filter, null, null, null, include).SingleOrDefaultAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateChangedPropertiesOnlyAsync(T entity, params object[] keyValues)
        {
            var existing = await context.Set<T>().FindAsync(keyValues);
            if (existing == null)
            {
                throw new Exception($"Entity with key {keyValues.ToString()} not found");
            }
            this.context.Entry(existing).CurrentValues.SetValues(entity);
        }

        protected IQueryable<T> GetQueryable(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null, int? take = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = false)
        {
            IQueryable<T> query = context.Set<T>();

            query = ProcessFilter(filter, query);

            query = ProcessDisableTracking(disableTracking, query);

            query = ProcessIncludes(include, query);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        protected static IQueryable<T> ProcessFilter(Expression<Func<T, bool>> filter, IQueryable<T> query)
        {
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        protected static IQueryable<TEntity> ProcessDisableTracking<TEntity>(bool disableTracking, IQueryable<TEntity> query) where TEntity : class
        {
            if (disableTracking)
            {
                return query.AsNoTracking();
            }
            else
            {
                return query;
            }
        }

        protected static IQueryable<TEntity> ProcessTextSearchParameters<TEntity>(TextSearchParameters textSearchParameters, IQueryable<TEntity> query) where TEntity : class
        {
            if (textSearchParameters == null)
            {
                return query;
            }

            var textSearchNeeded = textSearchParameters.TextSearchNeeded();
            if (textSearchNeeded)
            {
                query = query.Where(textSearchParameters.GetDynamicLinqWhereClauseExpression());
            }

            return query;
        }

        protected static IQueryable<TEntity> ProcessSortingParameters<TEntity>(SortingParameters sortingParameters, IQueryable<TEntity> query) where TEntity : class
        {
            if (sortingParameters == null)
            {
                return query;
            }

            var sortingNeeded = sortingParameters.SortNeeded();
            if (sortingNeeded)
            {
                query = query.OrderBy(sortingParameters.GetDynamicLinqOrderByClauseExpression());
            }

            return query;
        }

        protected static IQueryable<TEntity> ProcessPagingParameters<TEntity>(PagingParameters pagingParameters, IQueryable<TEntity> query) where TEntity : class
        {
            if (pagingParameters == null || pagingParameters.ItemsPerPage == 0)
            {
                return query;
            }

            query = query.Skip(pagingParameters.GetSkipAmount());
            query = query.Take(pagingParameters.GetTakeAmount());

            return query;
        }

        protected static IQueryable<TEntity> ProcessIncludes<TEntity>(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, IQueryable<TEntity> query) where TEntity : class
        {
            if (include != null)
            {
                query = include(query);
            }

            return query;
        }
    }
}
