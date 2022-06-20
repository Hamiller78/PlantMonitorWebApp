using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using PlantMonitorWebApp.Server.Services.ImageManager;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace UnitTests
{
    public class BlobImageManagerTests
    {
        private ILogger<BlobImageManager> _logger;

        public BlobImageManagerTests()
        {
            _logger = new Mock<ILogger<BlobImageManager>>().Object;
        }


        [Fact]
        public void UploadImageFile()
        {
            IConfiguration conf = UseConfigWithSecrets();
            string connectionString = conf["ConnectionStrings:ImageBlobStorage"];
            BlobImageManager testManager = new BlobImageManager(connectionString, _logger);
            testManager.InitStorage();

            BinaryData imageData = new BinaryData(File.ReadAllBytes("./Testdata/CactusPic.png"));
            testManager.StoreImage("TestCactus.png", imageData);
        }

        [Fact]
        public void FetchImageFile()
        {
            IConfiguration conf = UseConfigWithSecrets();
            string connectionString = conf["ConnectionStrings:ImageBlobStorage"];
            BlobImageManager testManager = new BlobImageManager(connectionString, _logger);
            testManager.InitStorage();

            BinaryData imageData = testManager.FetchImage("TestCactus.png");
        }

        [Fact]
        public void DeleteImageFile()
        {
            IConfiguration conf = UseConfigWithSecrets();
            string connectionString = conf["ConnectionStrings:ImageBlobStorage"];
            BlobImageManager testManager = new BlobImageManager(connectionString, _logger);
            testManager.InitStorage();

            testManager.DeleteImage("TestCactus.png");
        }


        private IConfiguration UseConfigWithSecrets()
        {
            var config = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();
            return config;
        }

    }
}
