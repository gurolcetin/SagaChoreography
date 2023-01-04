using MassTransit;
using Microsoft.Extensions.Configuration;
using Shared;
using Stock.API.Consumers;
using Stock.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<OrderCreatedEventConsumer>();
    configure.AddConsumer<PaymentFailedEventConsumer>();
    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

        configurator.ReceiveEndpoint(RabbitMQSettings.Stock_OrderCreatedEventQueue, e => e.ConfigureConsumer<OrderCreatedEventConsumer>(context));
    });
});

var app = builder.Build();

using IServiceScope scope = app.Services.CreateScope();
MongoDbService mongoDbService = scope.ServiceProvider.GetRequiredService<MongoDbService>();

mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(new()
{
    ProductId = 21,
    Count = 200
});
mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(new()
{
    ProductId = 22,
    Count = 100
});
mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(new()
{
    ProductId = 23,
    Count = 50
});
mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(new()
{
    ProductId = 24,
    Count = 10
});
mongoDbService.GetCollection<Stock.API.Models.Stock>().InsertOne(new()
{
    ProductId = 25,
    Count = 30
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
