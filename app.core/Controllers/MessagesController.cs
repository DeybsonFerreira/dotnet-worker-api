using dotnet_worker.Data;
using dotnet_worker.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace dotnet_worker.Controllers;

[ApiController]
[Route("api/[controller]")]
[DisableController]
[ApiExplorerSettings(IgnoreApi = true)]
public class MessagesController : ControllerBase
{
    private readonly ILogger<MessagesController> _logger;
    private readonly IMessageRepository _messageRepository;

    public MessagesController(
        ILogger<MessagesController> logger,
        IMessageRepository messageRepository)
    {
        _logger = logger;
        _messageRepository = messageRepository;
    }

    [HttpGet()]
    public async Task<List<Messages>> GetNotReads()
    {
        List<Messages> list = await _messageRepository.GetNotReads();
        return list;
    }
}

public class DisableControllerAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        filterContext.Result = new BadRequestResult();
    }
}