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
        try
        {
            _logger.BeginScope(new Dictionary<string, string>
            {
                ["UserId_scope"] = request.UserId.ToString()
            });
            _logger.LogInformation("orders list requested");

            var orderResponses = _orderRepository.GetOrders(request.ThrowExceptions);
            return orderResponses.ToArray();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured");
            throw;
        }
    }
}