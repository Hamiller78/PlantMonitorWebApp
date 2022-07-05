using PlantMonitorWebApp.Repository;
using PlantMonitorWebApp.Server.Interfaces;

namespace PlantMonitorWebApp.Server.Services.ImageManager
{
    public class DatabaseImageManager : IDatabaseImagesManager
    {
        private PlantAppContext _dbContext;

        public DatabaseImageManager(PlantAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GenerateImageName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
