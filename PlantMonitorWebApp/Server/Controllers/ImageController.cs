using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using PlantMonitorWebApp.Server.Interfaces;

namespace PlantMonitorWebApp.Server.Controllers
{
    [Route("/Images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageStorageHandler _storageHandler;

        public ImageController(IImageStorageHandler storageHandler)
        {
            _storageHandler = storageHandler;
            _storageHandler.InitStorage();
        }

        [HttpGet("Fetch")]
        public IActionResult Item([FromQuery]string id)
        {
            try
            {
                BinaryData imageData = _storageHandler.FetchImage(id);
                FileContentResult result = new(imageData.ToArray(), "image/png");
                return result;

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
