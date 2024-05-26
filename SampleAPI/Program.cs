using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SampleAPI.Entities;
using SampleAPI.Manager;
using SampleAPI.Middlewares;
using SampleAPI.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var logger = new LoggerConfiguration().
    WriteTo.Console().
    WriteTo.File("Logs/SampleApi.txt", rollingInterval: RollingInterval.Day).
    MinimumLevel.Warning().CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SampleApiDbContext>(options => options.UseInMemoryDatabase(databaseName: "SampleDB").
ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
builder.Services.AddTransient<IOrderManager, OrderManager>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom Middleware for global exception Handling.
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
