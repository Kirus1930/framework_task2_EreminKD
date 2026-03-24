using ModularApp.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var services = new ServiceCollection();

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var enabledModules = config.GetSection("Modules").Get<List<string>>();

var loader = new ModuleLoader();
var allModules = loader.LoadModules();

var modules = allModules
    .Where(m => enabledModules.Contains(m.Name))
    .ToList();

foreach (var module in modules)
{
    foreach (var dep in module.Dependencies)
    {
        if (!enabledModules.Contains(dep))
        {
            Console.WriteLine($"Error: {module.Name} requires {dep}");
            return;
        }
    }
}

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
