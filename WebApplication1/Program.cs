using Serilog;
using Serilog.Enrichers.ActivityTags;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using WebApplication1.Controllers;
using WebApplication1.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(ConfigureLogger);

void ConfigureLogger(HostBuilderContext context, LoggerConfiguration loggerConfiguration) =>
    loggerConfiguration
        .Enrich.FromLogContext()
        .Enrich.WithActivityTags()
        .Enrich.WithExceptionDetails()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
        .MinimumLevel.Override("System", LogEventLevel.Error)
        .WriteTo.Async(sinkConfiguration =>
        {
            sinkConfiguration.Console(new CompactJsonFormatter());
        });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<EnrichActivityActionFilter>();
});
builder.Services.AddScoped<OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();