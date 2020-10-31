using DeviceMonitoring.Dto;
using DeviceMonitoring.Entities;
using DeviceMonitoring.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace DeviceMonitoring.Controllers
{
    [Route("api/[controller]/[action]/")]
    public class AdminController : Controller
    {
        private readonly IRepository _repo;

        public AdminController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> SetData([FromBody] UpdateDeviceSettingsModel model)
        {
            var setting = await _repo.Filter<DeviceSettings>(x => x.DeviceId == model.Id).FirstOrDefaultAsync();
            if (setting == default)
                return NotFound();

            var update = Builders<DeviceSettings>.Update.Set(x => x.UpdatedDt, DateTime.UtcNow);

            if (model.Flowhanac.HasValue)
                update = update.Set(x => x.Flowhanac, model.Flowhanac.Value);
            if (model.Flowmax.HasValue)
                update = update.Set(x => x.Flowmax, model.Flowmax.Value);
            if (model.Flowproc.HasValue)
                update = update.Set(x => x.Flowproc, model.Flowproc.Value);
            if (model.Dpgorcakic.HasValue)
                update = update.Set(x => x.Dpgorcakic, model.Dpgorcakic.Value);
            if (model.Kgorcakic.HasValue)
                update = update.Set(x => x.Kgorcakic, model.Kgorcakic.Value);
            if (model.Onoff.HasValue)
                update = update.Set(x => x.Onoff, model.Onoff.Value);
            if (model.Pressgorcakic.HasValue)
                update = update.Set(x => x.Pressgorcakic, model.Pressgorcakic.Value);
            if (model.Restart.HasValue)
                update = update.Set(x => x.Restart, model.Restart.Value);

            await _repo.UpdateOne(setting.Id, update);
            return Ok(true);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetData(string id)
        {
            var datas = await _repo.Filter<DeviceData>(x => x.DeviceId == id).SortByDescending(x => x.UpdatedDt).FirstOrDefaultAsync();
            DeviceDataViewModel result = null;
            if (datas != default)
                result = new DeviceDataViewModel
                {
                    _id = datas.Id,
                    Id = datas.DeviceId,
                    Dpdrac = datas.Dpdrac,
                    Dpgorcakic = datas.Dpgorcakic,
                    Dppastaci = datas.Dppastaci,
                    Flowpast = datas.Flowpast,
                    Flowsarqac = datas.Flowsarqac,
                    Flowhanac = datas.Flowhanac,
                    Flowmax = datas.Flowmax,
                    Flowproc = datas.Flowproc,
                    Kgorcakic = datas.Kgorcakic,
                    Selfonoff = datas.Selfonoff,
                    Onoff = datas.Onoff,
                    Pressgorcakic = datas.Pressgorcakic,
                    Presspastaci = datas.Presspastaci,
                    Date = datas.UpdatedDt,
                    Disconnected = datas.UpdatedDt.AddSeconds(60) <= DateTime.UtcNow
                };
            return Ok(result);
        }
    }
}