using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Shared.Models;

namespace PlantMonitorWebApp.Server.Controllers
{
    [Route("/Images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private const long MAXFILESIZE = 10 * 1024 * 1024;

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

        [HttpPost("Upload")]
        public IActionResult Upload(IFormFile file)
        {
            // TODO: Optimize data type for this and storage handler, probably FileStream to pass file content to StorageHandler
            // this is the buffered method, for larger files stream method is recommended
            if (file.Length > MAXFILESIZE)
            {
                return BadRequest("File is too big.");
            }
            if (!file.ContentType.StartsWith("image"))
            {
                return BadRequest("File is not an image.");
            }

            // TODO: Generate file name for blob storage
            string blobFileName = file.FileName;
            using (MemoryStream byteStream = new())
            {
                file.CopyTo(byteStream);
                byte[] fileBytes = byteStream.ToArray();
                _storageHandler.StoreImage(blobFileName, new BinaryData(fileBytes));
            };

            return Ok(blobFileName);
        }

    }
}
