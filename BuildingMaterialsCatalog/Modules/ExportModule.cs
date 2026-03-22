using BuildingMaterialsCatalog.Core;
using BuildingMaterialsCatalog.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMaterialsCatalog.Modules
{
    public class ExportModule : IModule
    {
        public string Name => "Export";

        public IEnumerable<string> Dependencies =>
            new[] { "Reporting" };

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ExportService>();
        }

        public void Initialize(IServiceProvider provider)
        {
            var export = provider.GetRequiredService<ExportService>();

            export.ExportToFile();
        }
    }
}