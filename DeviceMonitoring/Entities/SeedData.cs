using DeviceMonitoring.DbContext;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace DeviceMonitoring.Entities
{
    public class SeedData
    {
        public static async Task Seed()
        {
            var context = new MongoDbContext();

            if (!await context.GetCollection<DeviceSettings>().Find(x => x.DeviceId == "d3").AnyAsync())
            {
                await context.GetCollection<DeviceSettings>().InsertOneAsync(new DeviceSettings { DeviceId = "d3", Onoff = true, CreatedDt = DateTime.UtcNow, UpdatedDt = DateTime.UtcNow });
            }
            if (!await context.GetCollection<DeviceData>().Find(x => x.DeviceId == "d3").AnyAsync())
            {
                await context.GetCollection<DeviceData>().InsertOneAsync(new DeviceData { DeviceId = "d3", CreatedDt = DateTime.UtcNow, UpdatedDt = DateTime.UtcNow });
            }
        }
    }
}