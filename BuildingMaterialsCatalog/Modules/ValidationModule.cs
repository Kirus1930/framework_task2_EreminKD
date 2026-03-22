using BuildingMaterialsCatalog.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingMaterialsCatalog.Modules
{
    public class ValidationModule : IModule
    {
        public string Name => "Validation";

        public IEnumerable<string> Dependencies =>
            new[] { "Storage" };

        public void RegisterServices(IServiceCollection services)
        {
        }

        public void Initialize(IServiceProvider provider)
        {
            Console.WriteLine("Validation module initialized");
        }
    }
}