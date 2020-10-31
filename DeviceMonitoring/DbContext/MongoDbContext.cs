using DeviceMonitoring.ProjectDbContext;
using MongoDB.Driver;

namespace DeviceMonitoring.DbContext
{
    public class MongoDbContext : IDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext()
        {
            var client = new MongoClient(MongoDbConfig.ConnectionString);
            _db = client.GetDatabase(MongoDbConfig.Database);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _db.GetCollection<TEntity>(typeof(TEntity).Name);
        }
    }
}