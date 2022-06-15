using Azure.Storage.Blobs;
using PlantMonitorWebApp.Server.Interfaces;

namespace PlantMonitorWebApp.Server.Services.ImageManager
{
    public class BlobImageManager
    {
        public string ConnectionString { get; init; }

        private BlobContainerClient? _container = null;

        public BlobImageManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void CreateBlobContainer(string containerName)
        {
            BlobServiceClient serviceClient = new(ConnectionString);
            var container = serviceClient.CreateBlobContainer(containerName);
            _container = container.Value;
        }

        public void SetContainer(string containerName)
        {
            BlobServiceClient serviceClient = new(ConnectionString);
            _container = serviceClient.GetBlobContainerClient(containerName);
        }

        public void CreateTextBlob(string blobname, string content)
        {
            BinaryData contentData = new BinaryData(content);
            _container?.UploadBlob(blobname, contentData);
        }


        public void Test()
        {
            BlobServiceClient serviceClient = new("");
            string containerName = "plantImages";
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient("filename");
        }

    }
}
