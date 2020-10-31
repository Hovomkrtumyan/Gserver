using DeviceMonitoring.Context;
using DeviceMonitoring.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeviceMonitoring.Repositories
{
    public interface IRepository
    {
        #region Async
        Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        Task<IEnumerable<T>> GetAllAsNoTrackingAsync<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        Task<T> GetByIdAsync<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        Task<T> GetByIdAsNoTrackingAsync<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        Task<IEnumerable<T>> FilterAsNoTrackingAsync<T>(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        #endregion

        #region Sync
        IQueryable<T> GetAll<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        IQueryable<T> GetAllAsNoTracking<T>(params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        T GetById<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        T GetByIdAsNoTracking<T>(long id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        IQueryable<T> FilterAsNoTracking<T>(Expression<Func<T, bool>> query, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        #endregion

        Task<T> Create<T>(T entity) where T : BaseEntity;
        IList<T> CreateRange<T>(IList<T> entities) where T : BaseEntity;
        Task<bool> HardRemove<T>(long id) where T : BaseEntity;
        Task<bool> HardRemoveRange<T>(IList<long> ids) where T : BaseEntity;
        Task<int> SaveChanges();
        SqlDbContext GetContext();
    }
}