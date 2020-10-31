using MongoDB.Driver;

namespace DeviceMonitoring.ProjectDbContext
{
    public interface IDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>();
    }
}