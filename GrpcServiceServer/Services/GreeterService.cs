using Grpc.Core;
using Prometheus.Client;
using System.Diagnostics.Metrics;

namespace GrpcServiceServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly ICounter _counter;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
            _counter = Metrics
                        .DefaultFactory
                        .CreateCounter("requests_total", "Número total de requisições.");
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _counter.Inc(); // Incrementa em 1

            if (int.Parse(request.Count) > 0 && int.Parse(request.Count) % 50000 == 0)
                Console.WriteLine($"Received {request.Count} requests:{DateTime.Now:HH:mm:ss}");
            //_logger.LogInformation("Received request to say hello to {Name}", request.Name);

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
