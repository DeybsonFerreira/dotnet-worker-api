using app.Common.Models;
using dotnet_worker.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_worker.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]

public class RabbitMqController : ControllerBase
{
    private readonly ILogger<RabbitMqController> _logger;
    private readonly IQueuePublisherService _event;

    public RabbitMqController(
        ILogger<RabbitMqController> logger,
        IQueuePublisherService eventBus
        )
    {
        _logger = logger;
        _event = eventBus;
    }

    [HttpPost()]
    public IActionResult Send(MessageModel message)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _event.SendMessage(message);
        return Ok();
    }
}
