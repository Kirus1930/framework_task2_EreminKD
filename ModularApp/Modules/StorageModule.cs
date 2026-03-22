using ModularApp.Core;
using ModularApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ModularApp.Modules
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
            var s = provider.GetRequiredService<IStorageService>();
            s.Add("Cement");
            s.Add("Bricks");
            Console.WriteLine("Storage initialized");
        }
    }
}
