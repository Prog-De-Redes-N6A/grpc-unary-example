using Grpc.Core;
using GrpcServer;
using Google.Protobuf.WellKnownTypes;

namespace GrpcServer.Services
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
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<AddReply> AddNumbers(AddRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AddReply
            {
                Sum = request.NumA + request.NumB
            });
        }

        public override Task<UserListReply> GetUserList(Empty request, ServerCallContext context)
        {
            UserListReply reply = new UserListReply();
            reply.Users.Add(new User { Name = "Juan", Age = 22, Gender = Gender.Male });
            reply.Users.Add(new User { Name = "Maria", Age = 12, Gender = Gender.Female });
            reply.Users.Add(new User { Name = "Pablo", Age = 44, Gender = Gender.Male });
            reply.MaxAge = 44; // al ser opcional no tengo porque setearla

            return Task.FromResult(reply);
        }
    }
}
