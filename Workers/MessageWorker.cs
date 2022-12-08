using dotnet_worker.Data;
using dotnet_worker.Interfaces;

namespace dotnet_worker.Workers
{
    public class MessageWorker : BackgroundService
    {
        private readonly ILogger<MessageWorker> _logger;
        private IMessageRepository _messageRepository { get; set; }

        public MessageWorker(ILogger<MessageWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            IServiceScope scope = serviceProvider.CreateScope();
            _messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<Messages> list = await _messageRepository.GetNotReads();

                foreach (var item in list)
                    _logger.LogInformation($"LOG :: {item.Id} - {item.Text}");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }

}