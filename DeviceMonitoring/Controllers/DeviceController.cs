using DeviceMonitoring.Dto;
using DeviceMonitoring.Entities;
using DeviceMonitoring.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DeviceMonitoring.Helpers;

namespace DeviceMonitoring.Controllers
{
    [Route("[action]/")]
    public class DeviceController : Controller
    {
        private readonly IRepository _repo;

        public DeviceController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSettings(string id)
        {
            var setting = await _repo.Filter<DeviceSettings>(x => x.DeviceId == id).FirstOrDefaultAsync();
            if (setting == default)
                return NotFound();
            var result = new DeviceSettingsViewModel
            {
                _id = setting.Id,
                Id = setting.DeviceId,
                Date = setting.UpdatedDt,
                Flowhanac = setting.Flowhanac,
                Flowmax = setting.Flowmax,
                Flowproc = setting.Flowproc,
                Dpgorcakic = setting.Dpgorcakic,
                Kgorcakic = setting.Kgorcakic,
                FlowAutoOnoff = await _repo.FilterAsNoTracking<FlowSettings>(x => true).Select(x => x.On).FirstOrDefaultAsync(),
                Onoff = setting.Onoff,
                Pressgorcakic = setting.Pressgorcakic
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlowAuto()
        {
            var result = await _repo.GetAll<FlowSettings>().FirstOrDefaultAsync();
            return Ok(new FlowModel { Flowauto = result.On ? result.FlowAuto : default });
        }

        [HttpGet]
        public async Task<IActionResult> SetData(string id, string data)
        {
            var model = JsonSerializer.Deserialize<SetDataModel>(data);

            if (!await _repo.Filter<DeviceSettings>(x => x.DeviceId == id).AnyAsync())
                return NotFound();

            var result = new DeviceData
            {
                DeviceId = id,
                Flowpast = model.flowpast,
                Flowsarqac = model.flowsarqac,
                Flowhanac = model.flowhanac,
                Flowmax = model.flowmax,
                Flowproc = model.flowproc,
                Dppastaci = model.dppastaci,
                Dpdrac = model.dpdrac,
                Dpgorcakic = model.dpgorcakic,
                Kgorcakic = model.kgorcakic,
                Onoff = model.onoff,
                Selfonoff = model.selfonoff,
                Presspastaci = model.presspastaci,
                Pressgorcakic = model.pressgorcakic,
                CreatedDt = DateTime.UtcNow.ArmenianDateNow(),
                UpdatedDt = DateTime.UtcNow.ArmenianDateNow()
            };
            if (id == "d1")
            {
                var flowSettings = await _repo.GetAll<FlowSettings>().FirstOrDefaultAsync();
                if (flowSettings != default)
                {
                    flowSettings.FlowAuto = model.flowauto;
                    flowSettings.UpdatedDt = DateTime.UtcNow.ArmenianDateNow();
                }
            }
            var deviceDate = await _repo.Create(result);
            await _repo.SaveChanges();
            return Ok(deviceDate.Id);
        }
    }

    public class FlowModel
    {
        public double Flowauto { get; set; }
    }
}