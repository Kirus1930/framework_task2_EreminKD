using ModularApp.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var services = new ServiceCollection();

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var enabledModules = config.GetSection("Modules").Get<string[]>();

var loader = new ModuleLoader();

var enabledSet = enabledModules
    .Select(x => x.Trim().ToLower())
    .ToHashSet();

var modules = loader.LoadModules()
    .Where(m => enabledSet.Contains(m.Name.ToLower()))
    .ToList();

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

foreach (var m in enabledModules)
    Console.WriteLine($"CONFIG: '{m}'");

foreach (var m in loader.LoadModules())
    Console.WriteLine($"LOADED: '{m.Name}'");

Console.WriteLine("App started");
