using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PlantMonitorWebApp.Server.Interfaces;

namespace PlantMonitorWebApp.Server.Services.ImageManager
{
    public class BlobImageManager : IImageManager
    {
        private const string CONTAINERNAME = "images";

        public string ConnectionString { get; init; }
        private readonly ILogger<BlobImageManager> _logger;

        private BlobContainerClient? _containerClient = null;

        public BlobImageManager(string connectionString, ILogger<BlobImageManager> logger)
        {
            ConnectionString = connectionString;
            _logger = logger;
        }

        public void InitStorage()
        {
            try
            {
                BlobServiceClient serviceClient = new(ConnectionString);
                _containerClient = serviceClient.GetBlobContainerClient(CONTAINERNAME);
                _containerClient.CreateIfNotExists();
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "Error while checking for storage blob container.");
                throw;
            }
        }

        public void StoreImage(string imageName, BinaryData imageData)
        {
            if (_containerClient == null)
            {
                throw new ApplicationException("Attempt to store image in uninitialized storage.");
            }

            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(imageName);
                if (blobClient.Exists())
                {
                    _logger.LogError("Error while storing image: Image with name {ImageName} already exists in storage.", imageName);
                    throw new ApplicationException("Attempt to create image in storage with already existing name.");
                }
                _containerClient.UploadBlob(imageName, imageData);
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "Error while storing new image {ImageName}", imageName);
                throw;
            }
        }

        public BinaryData FetchImage(string imageName)
        {
            if (_containerClient == null)
            {
                throw new ApplicationException("Attempt to fetch image in uninitialized storage.");
            }

            try
            {

            BlobClient blobClient = _containerClient.GetBlobClient(imageName);
            if (!blobClient.Exists())
            {
                _logger.LogError("Error while fetching image: Image with name {ImageName} does not exist in storage.", imageName);
                throw new ApplicationException("Attempt to fetch image from storage which doesn't exist.");
            }
            BlobDownloadResult download = blobClient.DownloadContent();
            return download.Content;

            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "Error while fetching image {ImageName}", imageName);
                throw;
            }
        }

        public void DeleteImage(string imageName)
        {
            if (_containerClient == null)
            {
                throw new ApplicationException("Attempt to delete image in uninitialized storage.");
            }

            try
            {

                BlobClient blobClient = _containerClient.GetBlobClient(imageName);
                if (!blobClient.Exists())
                {
                    _logger.LogError("Error while deleting image: Image with name {ImageName} does not existin storage.", imageName);
                    throw new ApplicationException("Attempt to delete image from storage which doesn't exist.");
                }
                blobClient.Delete(DeleteSnapshotsOption.IncludeSnapshots);
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError(ex, "Error while deleting image {ImageName}", imageName);
                throw;
            }
        }
    }
}
