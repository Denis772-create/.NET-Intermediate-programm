using CatalogService.Application.Mappers;
using CatalogService.Application.Services;
using CatalogService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.CacheProfiles.Add("Default10", new CacheProfile() { Duration = 10 }); // 10 second cache
});

builder.Services.AddResponseCaching();
builder.Services.AddAutoMapper(typeof(CatalogItemProfile).Assembly);
builder.Services.AddScoped<ICatalogService, CatalogService.Application.Services.CatalogService>();

// Add Swagger services
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Main");

    options.UseSqlServer(connectionString, opts =>
    {
        opts.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName);
    });
});

// options
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = "Please refer to the errors property for additional details."
        };

        return new BadRequestObjectResult(problemDetails)
        {
            ContentTypes = { "application/problem+json", "application/problem+xml" }
        };
    };
});

var app = builder.Build();

app.UseExceptionHandler("/errors");
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    c.RoutePrefix = string.Empty;
});
app.UseResponseCaching();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    await context.Database.MigrateAsync();

    await new CatalogDbContextSeed().SeedAsync(context);
}
await app.RunAsync();