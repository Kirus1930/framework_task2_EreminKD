namespace BuildingMaterialsCatalog.Services
{
    public class ExportService
    {
        private readonly IStorageService _storage;

        public ExportService(IStorageService storage)
        {
            _storage = storage;
        }

        public void ExportToFile()
        {
            var items = _storage.GetAll();

            File.WriteAllLines("export.txt", items);

            Console.WriteLine("Data exported to export.txt");
        }
    }
}