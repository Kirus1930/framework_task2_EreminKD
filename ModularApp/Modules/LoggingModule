using ModularApp.Core;
using ModularApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ModularApp.Modules
{
    public class LoggingModule : IModule
    {
        public string Name => "Logging";

        public IEnumerable<string> Dependencies => new[] { "Storage" };

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<LoggingService>();
        }

        public void Initialize(IServiceProvider provider)
        {
            Console.WriteLine("Logging initialized");
        }
    }
}