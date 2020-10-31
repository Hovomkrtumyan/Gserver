using DeviceMonitoring.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeviceMonitoring.Repositories
{
    public interface IRepository
    {
        IFindFluent<TEntity, TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity;
        Task<TEntity> Add<TEntity>(TEntity item) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> AddMany<TEntity>(IEnumerable<TEntity> items) where TEntity : BaseEntity;
        Task<string> DeleteOne<TEntity>(string id) where TEntity : BaseEntity;
        Task<IEnumerable<string>> DeleteMany<TEntity>(IEnumerable<string> ids) where TEntity : BaseEntity;
        Task<bool> UpdateOne<TEntity>(string id, UpdateDefinition<TEntity> update) where TEntity : BaseEntity;
        Task<bool> UpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : BaseEntity;
        Task<bool> UpdateMany<TEntity>(IEnumerable<string> ids, UpdateDefinition<TEntity> update) where TEntity : BaseEntity;
        Task<bool> UpdateMany<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : BaseEntity;
    }
}