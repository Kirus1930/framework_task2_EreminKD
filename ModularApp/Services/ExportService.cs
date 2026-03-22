namespace ModularApp.Services
{
    public class ExportService
    {
        private readonly IStorageService _storage;
        public ExportService(IStorageService storage) => _storage = storage;

        public void ExportToFile()
        {
            File.WriteAllLines("export.txt", _storage.GetAll());
            Console.WriteLine("Exported to export.txt");
        }
    }
}
