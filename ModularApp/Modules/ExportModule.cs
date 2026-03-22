using ModularApp.Core;
using ModularApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ModularApp.Modules
{
    public class ExportModule : IModule
    {
        public string Name => "Export";
        public IEnumerable<string> Dependencies => new[] { "Reporting" };

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ExportService>();
        }

        public void Initialize(IServiceProvider provider)
        {
            provider.GetRequiredService<ExportService>().ExportToFile();
        }
    }
}
