using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AsyncAwait.Task2.CodeReviewChallenge;

public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}