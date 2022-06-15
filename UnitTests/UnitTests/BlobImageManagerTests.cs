using Microsoft.Extensions.Configuration;
using PlantMonitorWebApp.Server.Services.ImageManager;
using System.Linq;
using System.Reflection;
using Xunit;

namespace UnitTests
{
    public class BlobImageManagerTests
    {
        [Fact]
        public void CreateTextFile()
        {
            IConfiguration conf = UseConfigWithSecrets();
            string connectionString = conf["ConnectionStrings:ImageBlobStorage"];
            BlobImageManager testManager = new BlobImageManager(connectionString);
            testManager.SetContainer("testcontainer");
            testManager.CreateTextBlob("testtext.txt", "Inhalt meines Blobs");
        }

        [Fact]
        public void CreateContainer()
        {
            IConfiguration conf = UseConfigWithSecrets();
            string connectionString = conf["ConnectionStrings:ImageBlobStorage"];
            BlobImageManager testManager = new BlobImageManager(connectionString);
            // BlobImageManager testManager = new BlobImageManager("UseDevelopmentStorage=true");
            testManager.CreateBlobContainer("testcontainer");
        }

        private IConfiguration UseConfigWithSecrets()
        {
            var config = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();
            return config;
        }


    }
}
