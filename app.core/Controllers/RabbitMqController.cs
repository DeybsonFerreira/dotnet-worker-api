using app.Common.Models;
using dotnet_worker.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_worker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RabbitMqController : ControllerBase
{
    private readonly ILogger<RabbitMqController> _logger;
    private readonly IEventBusService _event;

    public RabbitMqController(
        ILogger<RabbitMqController> logger,
        IEventBusService eventBus
        )
    {
        _logger = logger;
        _event = eventBus;
    }

    [HttpPost()]
    public void Send()
    {
        MessageModel message = new MessageModel(0, "Mensagem de teste");
        _event.SendMessage(message);
    }
}
