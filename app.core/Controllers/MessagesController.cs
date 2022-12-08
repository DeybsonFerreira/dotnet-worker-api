using dotnet_worker.Data;
using dotnet_worker.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_worker.Controllers;

[ApiController]
[Route("api/[controller]")]
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
