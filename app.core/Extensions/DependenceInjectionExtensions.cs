using app.Common.Configurations;
using app.Common.Interfaces;
using dotnet_worker.Interfaces;
using dotnet_worker.Repository;
using dotnet_worker.Services;
using Microsoft.EntityFrameworkCore;

namespace dotnet_worker.Extensions
{
    public static class DependenceInjectionExtensions
    {
        public static IServiceCollection RegisterCustomDependences(this IServiceCollection service, IConfiguration config)
        {
            service.AddScoped<IMessageRepository, MessageRepository>();
            service.AddScoped<IEventBusService, EventBusService>();
            service.AddTransient<IBusConnection, RabbitMqConnection>();
            RegisterDbContext(service, config);
            return service;
        }

        public static void RegisterDbContext(IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<MyAppContext>(options => options.UseSqlServer(config.GetConnectionString("DatabaseConnection")));
        }
    }
}