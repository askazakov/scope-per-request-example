using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersListController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<OrdersListController> _logger;

    public OrdersListController(ILogger<OrdersListController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "GetOrdersList")]
    public IEnumerable<OrderResponse> Get([FromBody] OrdersListRequest request)
    {
        _logger.LogInformation("orders list requested");
        if (request.ThrowExceptions)
        {
            throw new ApplicationException("requested exception");
        }
        return Enumerable.Range(1, 5).Select(_ => new OrderResponse
            {
                OrderId = Guid.NewGuid(),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}