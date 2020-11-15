using DeviceMonitoring.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonitoring.Entities
{
    public class SeedData
    {
        public static async Task Seed()
        {
            using (var context = new SqlDbContext(new DbContextOptions<SqlDbContext>()))
            {
                if (!await context.DeviceSettings.Where(x => x.DeviceId == "d1").AnyAsync())
                {
                    await context.DeviceSettings.AddAsync(new DeviceSettings { DeviceId = "d1", Onoff = true, CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now });
                    await context.SaveChangesAsync();
                }
                if (!await context.DeviceSettings.Where(x => x.DeviceId == "d2").AnyAsync())
                {
                    await context.DeviceSettings.AddAsync(new DeviceSettings { DeviceId = "d2", Onoff = true, CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now });
                    await context.SaveChangesAsync();
                }
                if (!await context.DeviceData.Where(x => x.DeviceId == "d1").AnyAsync())
                {
                    await context.DeviceData.AddAsync(new DeviceData { DeviceId = "d1", CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now });
                    await context.SaveChangesAsync();
                }
                if (!await context.DeviceData.Where(x => x.DeviceId == "d2").AnyAsync())
                {
                    await context.DeviceData.AddAsync(new DeviceData { DeviceId = "d2", CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now });
                    await context.SaveChangesAsync();
                }

                if (!await context.FlowSettings.AnyAsync())
                {
                    await context.FlowSettings.AddAsync(new FlowSettings { CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now, On = true });
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}