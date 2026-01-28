using CategoryServices.Data;
using CategoryServices.Model;
using CategoryServices.Repo;
using CategoryServices.Services;
using Microsoft.EntityFrameworkCore;
using Shared.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Repository register
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Kafka Producer + MessageBus register
builder.Services.AddKafkaProducer<int, Category>(config =>
{
    config.BootstrapServers = "localhost:9092"; // Kafka broker address
    config.Topic = "category-events";           // Topic name
});
builder.Services.AddKafkaMessageBus();


builder.Services.AddDbContext<CategoryDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CategoryServiceConnection"));
});

var app = builder.Build();

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
