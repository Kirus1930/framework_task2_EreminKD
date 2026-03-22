namespace ModularApp.Services
{
    public interface IStorageService
    {
        void Add(string item);
        List<string> GetAll();
    }
}
