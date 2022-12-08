using app.Common.Models.Options;

namespace dotnet_worker.Extensions
{
    public static class RegisterOptionsExtensions
    {
        public static IServiceCollection RegisterCustomOptions(this IServiceCollection service, IConfiguration config)
        {
            IConfigurationSection rabbitMqOptions = config.GetSection(RabbitMqOptions.RabbitMQ);

            //options
            service.Configure<RabbitMqOptions>(rabbitMqOptions);
            service.AddSingleton(rabbitMqOptions.Get<RabbitMqOptions>());
            return service;
        }
    }
}