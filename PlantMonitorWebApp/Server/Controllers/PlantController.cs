using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Server.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("/Plants")]
    public class PlantController : ControllerBase
    {
        readonly IPlantRepository _plantRepo;
        readonly IImageStorageHandler _imageStorageHandler;
        readonly ILogger<PlantController> _logger;

        public PlantController(IPlantRepository plantRepo, IImageStorageHandler imageStorageHandler, ILogger<PlantController> logger)
        {
            (_plantRepo, _imageStorageHandler, _logger) = (plantRepo, imageStorageHandler, logger);
            _imageStorageHandler.InitStorage();
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [Authorize]
        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] Plant plant)
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

        [Authorize]
        [HttpPost("Update")]
        public IActionResult Update([FromBody] Plant plant)
        {
            try
            {
                DeleteOldImage(plant);
                // TODO: This is now inefficient, since the repo class will get the database entry again after getting it to check the imageUrl above
                _plantRepo.Update(plant);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
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

        private void DeleteOldImage(Plant plant)
        {
            // Delete the old image if it was changed
            // TODO: This is pretty ugly, the problem is that the database repository doesn't know about the blob storage handler
            // Perhaps the storage handler should go into the repository project?
            try
            {
                Plant? oldPlant = _plantRepo.GetById(plant.Id);
                if (oldPlant != null && oldPlant.ImageUrl != plant.ImageUrl)
                {
                    string imageUrl = oldPlant.ImageUrl;
                    string imageFileName = imageUrl.Split('?')[1].Split('&').Where(p => p.StartsWith("image=")).SingleOrDefault()?.Substring(6) ?? string.Empty;
                    if (!string.IsNullOrEmpty(imageFileName))
                    {
                        _imageStorageHandler.DeleteImage(imageFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Exception in PlantController while attempting to delete image file from storage.");
            }
        }
    }
}
