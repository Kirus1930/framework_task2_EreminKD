namespace ModularApp.Services
{
    public class InMemoryStorageService : IStorageService
    {
        private readonly List<string> _items = new();
        public void Add(string item) => _items.Add(item);
        public List<string> GetAll() => _items;
    }
}
