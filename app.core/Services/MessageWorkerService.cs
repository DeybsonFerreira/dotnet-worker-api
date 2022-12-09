using dotnet_worker.Interfaces;

namespace dotnet_worker.Services
{
    public class MessageWorkerService : BackgroundService
    {
        private readonly ILogger<MessageWorkerService> _logger;
        private IMessageRepository _messageRepository;

        public MessageWorkerService(ILogger<MessageWorkerService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            IServiceScope scope = serviceProvider.CreateScope();
            _messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // List<Messages> list = await _messageRepository.GetNotReads();

                // foreach (var item in list)
                //     _logger.LogInformation($"LOG :: {item.Id} - {item.Text}");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }

}