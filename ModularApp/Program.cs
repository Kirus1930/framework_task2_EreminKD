using ModularApp.Core;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

var loader = new ModuleLoader();
var modules = loader.LoadModules();

var resolver = new ModuleDependencyResolver();
List<IModule> ordered;

try
{
    ordered = resolver.Resolve(modules);
}
catch (ModuleException ex)
{
    Console.WriteLine(ex.Message);
    return;
}

foreach (var m in ordered)
    m.RegisterServices(services);

var provider = services.BuildServiceProvider();

foreach (var m in ordered)
    m.Initialize(provider);

Console.WriteLine("App started");
