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
            try
            {
                return Ok(_sensorRepo.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Item/{id}")]
        public IActionResult Item(int id)
        {
            try
            {
                Sensor? sensor = _sensorRepo.GetById(id);
                if (sensor == null)
                {
                    return NotFound();
                }
                return Ok(sensor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Insert")]
        public IActionResult Insert([FromBody]Sensor sensor)
        {
            try
            {
                _sensorRepo.Insert(sensor);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody]Sensor sensor)
        {
            try
            {
                _sensorRepo.Update(sensor);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _sensorRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}