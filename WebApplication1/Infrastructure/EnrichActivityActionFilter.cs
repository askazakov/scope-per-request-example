using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Infrastructure;

public interface IWithUserId
{
    Guid UserId { get; }
}

public class EnrichActivityActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var actionArgument in context.ActionArguments)
        {
            if (actionArgument.Value is IWithUserId withUserId)
            {
                Activity.Current?.AddTag(nameof(IWithUserId.UserId), withUserId.UserId.ToString());
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}