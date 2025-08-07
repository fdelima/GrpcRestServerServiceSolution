using Grpc.Core;

namespace GrpcServiceServer.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            if (request.Count > 0 && request.Count % 50000 == 0)
            {
                _logger.LogInformation($"Received {request.Count} requests: {DateTime.Now:HH: mm: ss} ");
                _logger.LogInformation("Received request to say hello to {Name}", request.Name);
            }
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
