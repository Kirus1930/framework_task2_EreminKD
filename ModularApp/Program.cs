using ModularApp.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var services = new ServiceCollection();

// Чтение config
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var enabledModules = config.GetSection("Modules").Get<List<string>>();

var loader = new ModuleLoader();
var allModules = loader.LoadModules();

// Фильтрация
var modules = allModules
    .Where(m => enabledModules.Contains(m.Name))
    .ToList();

// Проверка зависимостей в config
foreach (var module in modules)
{
    foreach (var dep in module.Dependencies)
    {
        if (!enabledModules.Contains(dep))
        {
            Console.WriteLine(
                $"Error: Module '{module.Name}' requires '{dep}', but it is not enabled.");
            return;
        }
    }
}

var resolver = new ModuleDependencyResolver();

List<IModule> orderedModules;

try
{
    orderedModules = resolver.Resolve(modules);
}
catch (ModuleException ex)
{
    Console.WriteLine(ex.Message);
    return;
}

// Регистрация сервисов
foreach (var module in orderedModules)
{
    module.RegisterServices(services);
}

var provider = services.BuildServiceProvider();

// Запуск модулей
foreach (var module in orderedModules)
{
    module.Initialize(provider);
}

Console.WriteLine("Application started successfully.");