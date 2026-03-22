namespace BuildingMaterialsCatalog.Services
{
    public class ReportService
    {
        private readonly IStorageService _storage;

        public ReportService(IStorageService storage)
        {
            _storage = storage;
        }

        public void PrintReport()
        {
            var items = _storage.GetAll();

            Console.WriteLine("REPORT");
            Console.WriteLine("------");

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}