using System.Text;
using app.Common.Interfaces;
using dotnet_worker.Utils;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace dotnet_worker.Services
{
    public class ConsumerEventBus
    {
        public static void Listener(IBusConnection bus)
        {
            var channel = bus.GetChannel();

            channel.QueueDeclare(queue: QueueNames.Messages,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: QueueNames.Messages, autoAck: true, consumer: consumer);
        }
    }
}