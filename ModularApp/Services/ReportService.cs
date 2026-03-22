namespace ModularApp.Services
{
    public class ReportService
    {
        private readonly IStorageService _storage;
        public ReportService(IStorageService storage) => _storage = storage;

        public void PrintReport()
        {
            Console.WriteLine("REPORT:");
            foreach (var i in _storage.GetAll())
                Console.WriteLine(i);
        }
    }
}
