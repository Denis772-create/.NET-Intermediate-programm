using System;
using System.Threading.Tasks;
using CloudServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Task2.CodeReviewChallenge.Headers;

namespace Task2.CodeReviewChallenge.Middleware;

public class StatisticMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IStatisticService _statisticService;

    public StatisticMiddleware(RequestDelegate next, IStatisticService statisticService)
    {
        _next = next;
        _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path;

        await _statisticService.RegisterVisitAsync(path);
        await UpdateHeaders(context, path);

        await _next(context);
    }

    private async Task UpdateHeaders(HttpContext context, string path)
    {
        context.Response.Headers.Add(
            CustomHttpHeaders.TotalPageVisits,
            (await _statisticService.GetVisitsCountAsync(path)).ToString());
    }
}
