using DeviceMonitoring.Context;
using DeviceMonitoring.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceMonitoring.Entities
{
    public static class SeedData
    {
        public static async Task Seed()
        {
            await using var context = new SqlDbContext(new DbContextOptions<SqlDbContext>());
            if (!await context.DeviceSettings.Where(x => x.DeviceId == "d3").AnyAsync())
            {
                await context.DeviceSettings.AddAsync(new DeviceSettings { DeviceId = "d3", Onoff = true, CreatedDt = DateTime.UtcNow.ArmenianDateNow(), UpdatedDt = DateTime.UtcNow.ArmenianDateNow() });
                await context.SaveChangesAsync();
            }
            if (!await context.DeviceData.Where(x => x.DeviceId == "d3").AnyAsync())
            {
                await context.DeviceData.AddAsync(new DeviceData { DeviceId = "d3", CreatedDt = DateTime.UtcNow.ArmenianDateNow(), UpdatedDt = DateTime.UtcNow.ArmenianDateNow() });
                await context.SaveChangesAsync();
            }
        }
    }
}