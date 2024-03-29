﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using PlantMonitorWebApp.Server.Services.ImageManager;
using System.Reflection;

namespace PlantMonitorWebApp.ManualTests
{
    public class BlobImageHandlerTests
    {
        private ILogger<ImageAzureBlobHandler> _logger;

        public BlobImageHandlerTests()
        {
            _logger = new Mock<ILogger<ImageAzureBlobHandler>>().Object;
        }


        [Fact]
        public void UploadImageFile()
        {
            IConfiguration conf = UseConfigWithSecrets();
            ImageAzureBlobHandler testManager = new (conf, _logger);
            testManager.InitStorage();

            BinaryData imageData = new BinaryData(File.ReadAllBytes("./Testdata/CactusPic.png"));
            testManager.StoreImage("img-1234", imageData);
        }

        [Fact]
        public void FetchImageFile()
        {
            IConfiguration conf = UseConfigWithSecrets();
            ImageAzureBlobHandler testManager = new (conf, _logger);
            testManager.InitStorage();

            BinaryData imageData = testManager.FetchImage("TestCactus.png");
        }

        [Fact]
        public void DeleteImageFile()
        {
            IConfiguration conf = UseConfigWithSecrets();
            ImageAzureBlobHandler testManager = new (conf, _logger);
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
