using BuildingMaterialsCatalog.Core;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

var loader = new ModuleLoader();

var modules = loader.LoadModules();

var resolver = new ModuleDependencyResolver();

List<IModule> orderedModules;

try
{
    orderedModules = resolver.Resolve(modules);
}
catch (ModuleException ex)
{
    Console.WriteLine("Module loading error:");
    Console.WriteLine(ex.Message);
    return;
}

foreach (var module in orderedModules)
{
    module.RegisterServices(services);
}

var provider = services.BuildServiceProvider();

foreach (var module in orderedModules)
{
    module.Initialize(provider);
}

Console.WriteLine("Application started successfully.");

/*
using BuildingMaterialsCatalog.Middleware;
using BuildingMaterialsCatalog.Services;
using BuildingMaterialsCatalog.Validators;
using BuildingMaterialsCatalog.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры
builder.Services.AddControllers();

// Регистрируем зависимости как синглтоны
builder.Services.AddSingleton<IStorageService, InMemoryStorageService>();
builder.Services.AddSingleton<BuildingMaterialValidator>(); // валидатор без состояния

// Настраиваем логирование (консоль)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Конвейер middleware (порядок важен!)
app.UseMiddleware<RequestLoggingMiddleware>();   // 1. логирование + requestId
app.UseMiddleware<ExceptionHandlingMiddleware>(); // 2. обработка ошибок (должен быть до остальных, чтобы ловить исключения)
app.UseMiddleware<ExecutionTimeMiddleware>();    // 3. измерение времени

app.UseRouting();
app.MapControllers();

// Добавляем начальные данные
using (var scope = app.Services.CreateScope())
{
    var storage = scope.ServiceProvider.GetRequiredService<IStorageService>();
  
    Task.Run(async () =>
    {
        await storage.CreateAsync(new BuildingMaterialCreateDto
        {
            Name = "Цемент М500",
            UnitOfMeasure = "kg",
            PricePerUnit = 50,
            QuantityInStock = 1000
        });
        await storage.CreateAsync(new BuildingMaterialCreateDto
        {
            Name = "Песок речной",
            UnitOfMeasure = "kg",
            PricePerUnit = 15,
            QuantityInStock = 5000
        });
    }).Wait();
}

app.Run();
*/