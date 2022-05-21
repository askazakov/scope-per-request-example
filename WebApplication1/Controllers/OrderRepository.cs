namespace WebApplication1.Controllers;

public class OrderRepository
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(ILogger<OrderRepository> logger)
    {
        _logger = logger;
    }

    public IEnumerable<OrderResponse> GetOrders()
    {
        _logger.LogInformation($"{nameof(GetOrders)} invoked");
        return Enumerable.Range(1, 5).Select(_ => new OrderResponse
        {
            OrderId = Guid.NewGuid(),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
    }
}