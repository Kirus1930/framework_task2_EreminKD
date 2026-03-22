using ModularApp.Core;
using ModularApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ModularApp.Modules
{
    public class ReportingModule : IModule
    {
        public string Name => "Reporting";
        public IEnumerable<string> Dependencies => new[] { "Storage" };

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ReportService>();
        }

        public void Initialize(IServiceProvider provider)
        {
            provider.GetRequiredService<ReportService>().PrintReport();
        }
    }
}
