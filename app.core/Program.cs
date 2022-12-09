using dotnet_worker.Extensions;
using dotnet_worker.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

//Register Dependences
builder.Services.RegisterCustomDependences(config);
builder.Services.RegisterCustomOptions(config);
builder.Services.RegisterRabbitMqConnection(config);

builder.Services.AddHostedService<QueueConsumerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

