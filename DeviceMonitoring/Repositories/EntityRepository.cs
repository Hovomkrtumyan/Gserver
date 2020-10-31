using DeviceMonitoring.Context;
using DeviceMonitoring.Entities;
using DeviceMonitoring.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeviceMonitoring.Repositories
{
    public class EntityRepository : IRepository
    {
        private readonly SqlDbContext _context;

        public EntityRepository(SqlDbContext context)
        {
            _context = context;
        }

        #region Async Read Part
        public async Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            var set = _context.Set<T>().AsQueryable();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return await set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsNoTrackingAsync<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            var set = _context.Set<T>().AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, include) => current.Include(include));
            return await set.ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            var set = _context.Set<T>().Where(x => x.Id == id);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return await set.FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsNoTrackingAsync<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            var set = _context.Set<T>().Where(x => x.Id == id).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return await set.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> FilterAsNoTrackingAsync<T>(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            if (query == null)
                throw new SmartException("Query is null");
            var set = _context.Set<T>().Where(query).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return await set.ToListAsync();
        }

        #endregion

        #region Sync Read Part

        public IQueryable<T> GetAll<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            var set = _context.Set<T>().AsQueryable();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }

        public IQueryable<T> GetAllAsNoTracking<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            var set = _context.Set<T>().AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }

        public T GetById<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            var set = _context.Set<T>().Where(entity => entity.Id == id);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set.FirstOrDefault();
        }

        public T GetByIdAsNoTracking<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            var set = _context.Set<T>().Where(entity => entity.Id == id).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set.FirstOrDefault();
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            if (query == null)
                throw new SmartException("Query is null");
            var set = _context.Set<T>().Where(query);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }

        public IQueryable<T> FilterAsNoTracking<T>(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            if (query == null)
                throw new SmartException("Query is null");
            var set = _context.Set<T>().Where(query).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }
        #endregion

        #region CUD Part
        public async Task<T> Create<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IList<T> CreateRange<T>(IList<T> entities) where T : BaseEntity
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<bool> HardRemove<T>(long id) where T : BaseEntity
        {
            try
            {
                var entityToRemove = await GetByIdAsync<T>(id);
                _context.Set<T>().Remove(entityToRemove);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> HardRemoveRange<T>(IList<long> ids) where T : BaseEntity
        {
            try
            {
                var entityToRemove = await Filter<T>(x => ids.Contains(x.Id)).ToListAsync();
                _context.Set<T>().RemoveRange(entityToRemove);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public SqlDbContext GetContext()
        {
            return _context;
        }
    }
}