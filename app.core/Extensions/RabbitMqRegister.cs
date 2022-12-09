using app.Common.Models;
using RabbitMQ.Client;

namespace dotnet_worker.Extensions
{
    public static class RabbitMqRegister
    {
        public static IServiceCollection RegisterRabbitMqConnection(this IServiceCollection service, IConfiguration config)
        {
            var rabbitMqOptions = config.GetSection(RabbitMqOptions.RabbitMQ);

            ConnectionFactory connectionFactory = GetConnectionFactory(rabbitMqOptions.Get<RabbitMqOptions>());
            service.AddSingleton<IConnectionFactory>(connectionFactory);
            return service;
        }

        private static ConnectionFactory GetConnectionFactory(RabbitMqOptions option)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = option.HostName,
                UserName = option.UserName,
                Password = option.Password,
                Port = AmqpTcpEndpoint.UseDefaultPort
            };
            return factory;
        }


    }
}