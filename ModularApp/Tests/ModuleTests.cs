using Xunit;
using ModularApp.Core;
using System.Collections.Generic;

namespace ModularApp.Tests
{
    public class ModuleTests
    {
        class A : IModule
        {
            public string Name => "A";
            public IEnumerable<string> Dependencies => new[] { "B" };
            public void RegisterServices(IServiceCollection s) { }
            public void Initialize(IServiceProvider p) { }
        }

        class B : IModule
        {
            public string Name => "B";
            public IEnumerable<string> Dependencies => new string[0];
            public void RegisterServices(IServiceCollection s) { }
            public void Initialize(IServiceProvider p) { }
        }

        [Fact]
        public void OrderTest()
        {
            var modules = new List<IModule> { new A(), new B() };
            var resolver = new ModuleDependencyResolver();
            var result = resolver.Resolve(modules);

            Assert.Equal("B", result[0].Name);
        }

        [Fact]
        public void MissingTest()
        {
            var modules = new List<IModule> { new A() };
            var resolver = new ModuleDependencyResolver();

            Assert.Throws<ModuleException>(() => resolver.Resolve(modules));
        }
    }
}
