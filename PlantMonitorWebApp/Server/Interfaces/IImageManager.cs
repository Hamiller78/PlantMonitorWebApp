namespace PlantMonitorWebApp.Server.Interfaces
{
    public interface IImageManager
    {
        void InitStorage();
        BinaryData FetchImage(string imageName);
        void StoreImage(string imageName, BinaryData imageData);
        void DeleteImage(string imageName);

    }
}
