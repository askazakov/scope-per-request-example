using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersListController : ControllerBase
{
    private readonly OrderRepository _orderRepository;
    private readonly ILogger<OrdersListController> _logger;

    public OrdersListController(
        OrderRepository orderRepository,
        ILogger<OrdersListController> logger)
    {
        _orderRepository = orderRepository;
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

        var orderResponses = _orderRepository.GetOrders();
        return orderResponses.ToArray();
    }
}