using ModularApp.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ModularApp.Tests
{
    public class A : IModule
    {
        public string Name => "A";
        public IEnumerable<string> Dependencies => new[] { "B" };
        public void RegisterServices(IServiceCollection s) { }
        public void Initialize(IServiceProvider p) { }
    }

    public class B : IModule
    {
        public string Name => "B";
        public IEnumerable<string> Dependencies => Array.Empty<string>();
        public void RegisterServices(IServiceCollection s) { }
        public void Initialize(IServiceProvider p) { }
    }

    public class CycleA : IModule
    {
        public string Name => "CycleA";
        public IEnumerable<string> Dependencies => new[] { "CycleB" };
        public void RegisterServices(IServiceCollection s) { }
        public void Initialize(IServiceProvider p) { }
    }

    public class CycleB : IModule
    {
        public string Name => "CycleB";
        public IEnumerable<string> Dependencies => new[] { "CycleA" };
        public void RegisterServices(IServiceCollection s) { }
        public void Initialize(IServiceProvider p) { }
    }
}
