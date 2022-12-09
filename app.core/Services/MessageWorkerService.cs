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
                var qtd = await _messageRepository.Size();
                _logger.LogInformation($"Total de {qtd} Mensagens cadastradas");

                await Task.Delay(5000, stoppingToken);
            }
        }
    }

}