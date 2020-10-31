﻿using DeviceMonitoring.Dto;
using DeviceMonitoring.Entities;
using DeviceMonitoring.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

            setting.UpdatedDt = DateTime.Now;

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
            if (model.Restart.HasValue)
                setting.Restart = model.Restart.Value;

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
                    Disconnected = data.UpdatedDt.AddSeconds(60) <= DateTime.Now
                };
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitoringResult(string id)
        {
            var data = await _repo.Filter<DeviceData>(x => x.DeviceId == id && x.UpdatedDt.Month == DateTime.Now.Month)
                .OrderBy(x => x.UpdatedDt).ToListAsync();

            var resultFlowPast = 0d;
            var resultFlowsarqac = 0d;

            for (var i = 0; i < data.Count; i++)
            {
                if (i + 1 == data.Count)
                    break;
                var seconds = (data[i + 1].UpdatedDt - data[i].UpdatedDt).Seconds;
                resultFlowPast += seconds * data[i].Flowpast;
                resultFlowsarqac += seconds * data[i].Flowsarqac;
            }
            return Ok();
        }
    }
}