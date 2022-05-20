using WebApplication1.Infrastructure;

namespace WebApplication1.Controllers;

public class ForecastRequest : IWithUserId
{
    public Guid UserId { get; init; }
    public bool ThrowExceptions { get; init; }
}