using Microsoft.AspNetCore.Builder;
using Task2.CodeReviewChallenge.Middleware;

namespace Task2.CodeReviewChallenge.Extensions;

public static class LoggingRequestMiddlewareExtensions
{
    public static IApplicationBuilder UseStatistic(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<StatisticMiddleware>();
    }
}
