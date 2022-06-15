namespace PlantMonitorWebApp.Server.Interfaces
{
    public interface IImageManager
    {
        Uri FetchImage(string id);
        void StoreImage(); //Pass image in which format? Byte array?

    }
}
