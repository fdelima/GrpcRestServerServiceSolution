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
            if (int.Parse(request.Count) > 0 && int.Parse(request.Count) % 100000 == 0)
                Console.WriteLine($"Received 100000 requests:{DateTime.Now:HHmmss}");
            //_logger.LogInformation("Received request to say hello to {Name}", request.Name);

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
