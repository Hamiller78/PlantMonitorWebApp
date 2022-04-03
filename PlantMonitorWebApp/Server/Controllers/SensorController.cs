using Microsoft.AspNetCore.Mvc;

using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Server.Controllers
{
    [ApiController]
    [Route("/Sensors")]
    public class SensorController : ControllerBase
    {
        readonly ISensorRepository _sensorRepo;

        public SensorController(ISensorRepository sensorRepo)
            => _sensorRepo = sensorRepo;

        [HttpGet("GetList")]
        public IActionResult List()
        {
            return Ok(_sensorRepo.GetAll());
        }

        [HttpGet("Item/{id}")]
        public IActionResult Item(int id)
        {
            Sensor? sensor = _sensorRepo.GetById(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return Ok(sensor);
        }
    }
}
