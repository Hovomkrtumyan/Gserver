using DeviceMonitoring.Entities;
using DeviceMonitoring.ProjectDbContext;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeviceMonitoring.Repositories
{
    public class EntityRepository : IRepository
    {
        private readonly IDbContext _dbContext;

        public EntityRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IFindFluent<TEntity, TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : BaseEntity
        {
            var collection = GetCollection<TEntity>();
            var cursor = collection.Find(filter);
            return cursor;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : BaseEntity
        {
            var collection = GetCollection<TEntity>();
            var entities = await collection.Find(new BsonDocument()).ToListAsync();
            return entities;
        }

        public async Task<TEntity> Add<TEntity>(TEntity item) where TEntity : BaseEntity
        {
            var collection = GetCollection<TEntity>();
            item.CreatedDt = DateTime.UtcNow;
            item.UpdatedDt = DateTime.UtcNow;
            await collection.InsertOneAsync(item);
            return item;
        }

        public async Task<IEnumerable<TEntity>> AddMany<TEntity>(IEnumerable<TEntity> items) where TEntity : BaseEntity
        {
            var collection = GetCollection<TEntity>();
            var addMany = items as TEntity[] ?? items.ToArray();

            foreach (var item in addMany)
            {
                item.CreatedDt = DateTime.UtcNow;
                item.UpdatedDt = DateTime.UtcNow;
            }
            await collection.InsertManyAsync(addMany);

            return addMany;
        }

        public async Task<string> DeleteOne<TEntity>(string id) where TEntity : BaseEntity
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq(EntityConstants.Id, ObjectId.Parse(id));
            return await DeleteOne(filter) ? id : null;
        }

        public async Task<IEnumerable<string>> DeleteMany<TEntity>(IEnumerable<string> ids) where TEntity : BaseEntity
        {
            var objectIds = ids.Select(id => new ObjectId(id)).ToList();
            var filter = Builders<TEntity>.Filter.In(EntityConstants.Id, objectIds);

            return await DeleteMany(filter) ? ids : null;
        }

        public async Task<bool> UpdateOne<TEntity>(string id, UpdateDefinition<TEntity> update) where TEntity : BaseEntity
        {
            var filter = Builders<TEntity>.Filter.Eq(EntityConstants.Id, new ObjectId(id));
            return await UpdateOne(filter, update);
        }

        public async Task<bool> UpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : BaseEntity
        {
            var collection = GetCollection<TEntity>();
            var updateRes = await collection.UpdateOneAsync(filter, update);
            return updateRes.IsAcknowledged;
        }

        public async Task<bool> UpdateMany<TEntity>(IEnumerable<string> ids, UpdateDefinition<TEntity> update) where TEntity : BaseEntity
        {
            var filter = new FilterDefinitionBuilder<TEntity>().In(EntityConstants.Id, ids);
            return await UpdateOne(filter, update);
        }

        public async Task<bool> UpdateMany<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : BaseEntity
        {
            var collection = GetCollection<TEntity>();
            var updateRes = await collection.UpdateManyAsync(filter, update);
            return true;
        }

        #region Private
        private IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _dbContext.GetCollection<TEntity>();
        }

        private async Task<bool> DeleteOne<TEntity>(FilterDefinition<TEntity> filter) where TEntity : BaseEntity
        {
            var collection = GetCollection<TEntity>();
            var deleteRes = await collection.DeleteOneAsync(filter);
            return deleteRes.IsAcknowledged;
        }

        public async Task<bool> DeleteMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : BaseEntity
        {
            var collection = GetCollection<TEntity>();
            var deleteRes = await collection.DeleteManyAsync(filter);
            return deleteRes.IsAcknowledged;
        }
        #endregion
    }
}
