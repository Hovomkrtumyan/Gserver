using DeviceMonitoring.Dto;
using DeviceMonitoring.Entities;
using DeviceMonitoring.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using DeviceMonitoring.Helpers;

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

            setting.UpdatedDt = DateTime.UtcNow.ArmenianDateNow();

            if (model.Flowhanac.HasValue)
                setting.Flowhanac = model.Flowhanac.Value;
            if (model.Flowmax.HasValue)
                setting.Flowmax = model.Flowmax.Value;
            if (model.Flowproc.HasValue)
                setting.Flowproc = model.Flowproc.Value;
            if (model.Dpgorcakic.HasValue)
                setting.Dpgorcakic = model.Dpgorcakic.Value;
            if (model.Kgorcakic.HasValue)
                setting.Kgorcakic = model.Kgorcakic.Value;
            if (model.Onoff.HasValue)
                setting.Onoff = model.Onoff.Value;
            if (model.Pressgorcakic.HasValue)
                setting.Pressgorcakic = model.Pressgorcakic.Value;

            await _repo.SaveChanges();
            return Ok(true);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetData(string id)
        {
            var data = await _repo.Filter<DeviceData>(x => x.DeviceId == id).OrderByDescending(x => x.UpdatedDt).FirstOrDefaultAsync();
            DeviceDataViewModel result = null;
            if (data != default)
                result = new DeviceDataViewModel
                {
                    _id = data.Id,
                    Id = data.DeviceId,
                    Dpdrac = data.Dpdrac,
                    Dpgorcakic = data.Dpgorcakic,
                    Dppastaci = data.Dppastaci,
                    Flowpast = data.Flowpast,
                    Flowsarqac = data.Flowsarqac,
                    Flowhanac = data.Flowhanac,
                    Flowmax = data.Flowmax,
                    Flowproc = data.Flowproc,
                    Kgorcakic = data.Kgorcakic,
                    Selfonoff = data.Selfonoff,
                    Onoff = data.Onoff,
                    Pressgorcakic = data.Pressgorcakic,
                    Presspastaci = data.Presspastaci,
                    Date = data.UpdatedDt,
                    Disconnected = data.UpdatedDt.AddSeconds(60) <= DateTime.UtcNow.ArmenianDateNow()
                };
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitoringResult(string id)
        {
            var todayData = await _repo.FilterAsNoTracking<DeviceData>(x => x.DeviceId == id && x.UpdatedDt.Day == DateTime.UtcNow.ArmenianDateNow().Day && x.UpdatedDt.Hour >= 11)
                .OrderBy(x => x.UpdatedDt).Select(x => new { x.UpdatedDt, x.Flowpast, x.Flowsarqac }).ToListAsync();
            var monthBegin = new DateTime(DateTime.UtcNow.ArmenianDateNow().Year, DateTime.UtcNow.ArmenianDateNow().Month, 1, 11, 00, 00);
            var monthData = await _repo.Filter<DeviceData>(x => x.DeviceId == id && x.UpdatedDt.Month == DateTime.UtcNow.ArmenianDateNow().Month && x.UpdatedDt >= monthBegin)
                .OrderBy(x => x.UpdatedDt).Select(x => new { x.UpdatedDt, x.Flowpast, x.Flowsarqac }).ToListAsync();

            var result = new MonitoringModel();

            for (var i = 0; i < todayData.Count; i++)
            {
                if (i + 1 == todayData.Count)
                    break;
                var hours = (todayData[i + 1].UpdatedDt - todayData[i].UpdatedDt).TotalHours;
                result.Orekan1kwpast += hours * todayData[i].Flowpast;
                result.Orekan1kashx += hours * todayData[i].Flowsarqac;
            }

            for (var i = 0; i < monthData.Count; i++)
            {
                if (i + 1 == monthData.Count)
                    break;
                var hours = (monthData[i + 1].UpdatedDt - monthData[i].UpdatedDt).TotalHours;
                result.Amsekan1kwpast += hours * monthData[i].Flowpast;
                result.Amsekan1kwashx += hours * monthData[i].Flowsarqac;
            }

            result.Orekan1kashx = Math.Round(result.Orekan1kashx, 2);
            result.Orekan1kwpast = Math.Round(result.Orekan1kwpast, 2);
            result.Amsekan1kwashx = Math.Round(result.Amsekan1kwashx, 2);
            result.Amsekan1kwpast = Math.Round(result.Amsekan1kwpast, 2);
            return Ok(result);
        }
    }
}