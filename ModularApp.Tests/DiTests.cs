using System;
using System.Reflection;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using ModularApp.Services;

namespace ModularApp.Tests
{
    public class DiTests
    {
        [Fact]
        public void Should_Inject_Dependency_Through_DI()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IStorageService, InMemoryStorageService>();
            services.AddSingleton<ReportService>();

            var provider = services.BuildServiceProvider();

            var reportService = provider.GetRequiredService<ReportService>();
            var storageService = provider.GetRequiredService<IStorageService>();

            // Получаем приватное поле _storage через reflection
            var field = typeof(ReportService)
                .GetField("_storage", BindingFlags.NonPublic | BindingFlags.Instance);

            var injectedStorage = field.GetValue(reportService);

            Assert.NotNull(injectedStorage);
            Assert.Equal(storageService, injectedStorage);
        }
    }
}