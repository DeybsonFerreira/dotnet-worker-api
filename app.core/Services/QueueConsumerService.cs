using System.Text;
using app.Common.Interfaces;
using dotnet_worker.Data;
using dotnet_worker.Interfaces;
using dotnet_worker.Utils;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace dotnet_worker.Services
{
    public class QueueConsumerService : BackgroundService, IQueueConsumerService
    {
        private IBusConnection _bus;
        private IMessageRepository _messageRepository;

        public QueueConsumerService(IBusConnection bus, IServiceProvider serviceProvider)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            IServiceScope scope = serviceProvider.CreateScope();
            _messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int second = 3;
            while (!stoppingToken.IsCancellationRequested)
            {
                ListenQueue();
                await Task.Delay(1000 * second, stoppingToken);
            }
        }

        public void ListenQueue()
        {
            Console.WriteLine("listening rabbitMQ");
            IModel _channel = _bus.ConnectChannel();

            _channel.QueueDeclare(queue: QueueNames.Messages,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (object sender, BasicDeliverEventArgs eventArgs) =>
                    {
                        var body = eventArgs.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Mensagem recebida : {message}");

                        try
                        {
                            var modelToSave = JsonConvert.DeserializeObject<Messages>(message);
                            await _messageRepository.SaveAsync(modelToSave);
                            _channel.BasicAck(eventArgs.DeliveryTag, false);
                        }
                        catch
                        {
                            _channel.BasicNack(eventArgs.DeliveryTag, false, true);
                        }

                    };

            _channel.BasicConsume(QueueNames.Messages, autoAck: false, consumer: consumer);
        }

    }
}