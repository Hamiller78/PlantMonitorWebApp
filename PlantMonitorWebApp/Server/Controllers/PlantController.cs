using Microsoft.AspNetCore.Mvc;

using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Server.Controllers
{
    [ApiController]
    [Route("/Plants")]
    public class PlantController : ControllerBase
    {
        readonly IPlantRepository _plantRepo;

        public PlantController(IPlantRepository plantRepo)
            => _plantRepo = plantRepo;

        [HttpGet("GetList")]
        public IActionResult List()
        {
            try
            {
                return Ok(_plantRepo.GetAll());
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
                Plant? plant = _plantRepo.GetById(id);
                if (plant == null)
                {
                    return NotFound();
                }
                return Ok(plant);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("Insert")]
        public IActionResult Insert([FromBody]Plant plant)
        {
            try
            {
                _plantRepo.Insert(plant);  // Currently we use the model class directly instead of a data transfer object. That means that a referenced sensor object may also be created from the JSON.
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody]Plant plant)
        {
            try
            {
                _plantRepo.Update(plant);
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
                _plantRepo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
