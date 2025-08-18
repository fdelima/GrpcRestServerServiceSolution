using Prometheus.Client;
using Prometheus.Client.MetricServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Observabilidade :: M�tricas
var options = new MetricServerOptions
{
    Port = 9092,
    MapPath = "/metrics",
};

var metricServer = new MetricServer(options);
metricServer.Start();

ICounter _requestsCounter;
_requestsCounter = Metrics
                      .DefaultFactory
                      .CreateCounter("requests_total", "Numero total de requisicoes.");

app.MapGet("/weatherforecast/{i}", (int i) =>
{
    _requestsCounter.Inc(); // Incrementa em 1

    if (i > 1 && i % 5000 == 0)
        Console.WriteLine($"Received {i} requests:{DateTime.Now:HH:mm:ss}");

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
