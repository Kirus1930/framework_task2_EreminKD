using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ModularApp.Core;

namespace ModularApp.Tests
{
    public class ModuleDependencyTests
    {
        [Fact]
        public void Should_Resolve_Order_Correctly()
        {
            var modules = new List<IModule>
            {
                new ModuleA(),
                new ModuleB(),
                new ModuleC()
            };

            var resolver = new ModuleDependencyResolver();
            var result = resolver.Resolve(modules);

            var names = result.Select(m => m.Name).ToList();

            Assert.Equal(new List<string> { "C", "B", "A" }, names);
        }

        [Fact]
        public void Should_Throw_When_Module_Missing()
        {
            var modules = new List<IModule>
            {
                new ModuleA() // требует B
            };

            var resolver = new ModuleDependencyResolver();

            var ex = Assert.Throws<ModuleException>(() => resolver.Resolve(modules));

            Assert.Contains("Missing module", ex.Message);
        }

        [Fact]
        public void Should_Throw_On_Cycle()
        {
            var modules = new List<IModule>
            {
                new CycleA(),
                new CycleB()
            };

            var resolver = new ModuleDependencyResolver();

            var ex = Assert.Throws<ModuleException>(() => resolver.Resolve(modules));

            Assert.Contains("Cycle", ex.Message);
        }
    }
}