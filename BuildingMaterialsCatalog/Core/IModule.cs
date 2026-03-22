using Microsoft.Extensions.DependencyInjection;

namespace BuildingMaterialsCatalog.Core
{
    public interface IModule
    {
        string Name { get; }

        IEnumerable<string> Dependencies { get; }

        void RegisterServices(IServiceCollection services);

        void Initialize(IServiceProvider provider);
    }
}