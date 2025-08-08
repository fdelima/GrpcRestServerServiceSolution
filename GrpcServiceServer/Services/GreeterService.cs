using Grpc.Core;
using Prometheus.Client;

namespace GrpcServiceServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly ICounter _requestsCounter;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
            _requestsCounter = Metrics
                        .DefaultFactory
                        .CreateCounter("grpcserviceserver_requests_total", "Numero total de requisicoes.");
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _requestsCounter.Inc(); // Incrementa em 1
            
            if (request.Count > 1 && request.Count % 50000 == 0)
                Console.WriteLine($"Received {request.Count} requests: {DateTime.Now:HH:mm:ss}");

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
