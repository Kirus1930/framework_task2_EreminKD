using Xunit;
using ModularApp.Core;
using System.Collections.Generic;

namespace ModularApp.Tests
{
    public class ModuleTests
    {
        [Fact]
        public void Order_ShouldBeCorrect()
        {
            var modules = new List<IModule> { new A(), new B() };
            var resolver = new ModuleDependencyResolver();
            var result = resolver.Resolve(modules);

            Assert.True(result[0].Name == "B");
        }

        [Fact]
        public void MissingModule_ShouldThrow()
        {
            var modules = new List<IModule> { new A() };
            var resolver = new ModuleDependencyResolver();

            Assert.Throws<ModuleException>(() => resolver.Resolve(modules));
        }

        [Fact]
        public void Cycle_ShouldThrow()
        {
            var modules = new List<IModule> { new CycleA(), new CycleB() };
            var resolver = new ModuleDependencyResolver();

            Assert.Throws<ModuleException>(() => resolver.Resolve(modules));
        }
    }
}
