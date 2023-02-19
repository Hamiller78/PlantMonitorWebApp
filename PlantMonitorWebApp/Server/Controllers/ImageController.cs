using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MimeMapping;
using System.IO;

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

        [AllowAnonymous]
        [HttpGet("Fetch")]
        public IActionResult Item([FromQuery]string image)
        {
            try
            {
                BinaryData imageData = _storageHandler.FetchImage(image);
                string mimeType = MimeUtility.GetMimeMapping(image);
                FileContentResult result = new(imageData.ToArray(), mimeType);
                return result;

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
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

            string blobFileName = GenerateFileName(file);
            using (MemoryStream byteStream = new())
            {
                file.CopyTo(byteStream);
                BinaryData fileBytes = new(byteStream.ToArray());
                _storageHandler.StoreImage(blobFileName, fileBytes);
            };

            return Ok(blobFileName);
        }

        private string GenerateFileName(IFormFile file)
        {
            string randomName = Guid.NewGuid().ToString();
            string extension = MimeUtility.GetExtensions(file.ContentType)[0];
            return randomName + "." + extension;
        }

    }
}
