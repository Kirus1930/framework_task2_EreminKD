using BuildingMaterialsCatalog.Core;
using BuildingMaterialsCatalog.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMaterialsCatalog.Modules
{
    public class ReportingModule : IModule
    {
        public string Name => "Reporting";

        public IEnumerable<string> Dependencies =>
            new[] { "Storage" };

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ReportService>();
        }

        public void Initialize(IServiceProvider provider)
        {
            var report = provider.GetRequiredService<ReportService>();

            report.PrintReport();
        }
    }
}