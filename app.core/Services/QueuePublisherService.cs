using System.Text;
using System.Text.Json;
using app.Common.Interfaces;
using app.Common.Models;
using dotnet_worker.Interfaces;
using dotnet_worker.Utils;
using RabbitMQ.Client;

namespace dotnet_worker.Services
{
    public class QueuePublisherService : IQueuePublisherService
    {
        protected IBusConnection _bus;
        private RabbitMqOptions _options;

        public QueuePublisherService(IBusConnection bus, RabbitMqOptions options)
        {
            _bus = bus;
            _options = options;
            CreateChanel();
        }

        public void SendMessage(object myObject)
        {
            var jsonData = JsonSerializer.Serialize(myObject);
            var body = Encoding.UTF8.GetBytes(jsonData);
            var channel = _bus.ConnectChannel();
            var prop = channel.CreateBasicProperties();

            channel.BasicPublish(
                exchange: "",
                routingKey: QueueNames.Messages,
                basicProperties: prop,
                body: body
            );
            Console.WriteLine($"Item enviado para a fila {_options.Exchange}");
        }

        public void CreateChanel()
        {
            _bus.ConnectChannel().QueueDeclare(
                queue: QueueNames.Messages,
                durable: false, //permanece ativo após o servidor ser reiniciado
                exclusive: false, // só pode ser acessada via conexão atual
                autoDelete: false //deletar automatic após consumir a fila
            );
        }
    }
}