using ModularApp.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ModularApp.Tests
{
    // A зависит от B
    public class ModuleA : IModule
    {
        public string Name => "A";

        public IEnumerable<string> Dependencies => new[] { "B" };

        public void RegisterServices(IServiceCollection services) { }

        public void Initialize(IServiceProvider provider) { }
    }

    // B зависит от C
    public class ModuleB : IModule
    {
        public string Name => "B";

        public IEnumerable<string> Dependencies => new[] { "C" };

        public void RegisterServices(IServiceCollection services) { }

        public void Initialize(IServiceProvider provider) { }
    }

    // C без зависимостей
    public class ModuleC : IModule
    {
        public string Name => "C";

        public IEnumerable<string> Dependencies => Array.Empty<string>();

        public void RegisterServices(IServiceCollection services) { }

        public void Initialize(IServiceProvider provider) { }
    }

    // Цикл A -> B -> A
    public class CycleA : IModule
    {
        public string Name => "CycleA";

        public IEnumerable<string> Dependencies => new[] { "CycleB" };

        public void RegisterServices(IServiceCollection services) { }

        public void Initialize(IServiceProvider provider) { }
    }

    public class CycleB : IModule
    {
        public string Name => "CycleB";

        public IEnumerable<string> Dependencies => new[] { "CycleA" };

        public void RegisterServices(IServiceCollection services) { }

        public void Initialize(IServiceProvider provider) { }
    }
}