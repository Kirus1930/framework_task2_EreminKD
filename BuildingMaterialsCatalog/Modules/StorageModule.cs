using BuildingMaterialsCatalog.Core;
using BuildingMaterialsCatalog.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMaterialsCatalog.Modules
{
    public class StorageModule : IModule
    {
        public string Name => "Storage";

        public IEnumerable<string> Dependencies => Array.Empty<string>();

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IStorageService, InMemoryStorageService>();
        }

        public void Initialize(IServiceProvider provider)
        {
            var storage = provider.GetRequiredService<IStorageService>();

            storage.Add("Cement");
            storage.Add("Bricks");
            storage.Add("Wood");

            Console.WriteLine("Storage module initialized");
        }
    }
}