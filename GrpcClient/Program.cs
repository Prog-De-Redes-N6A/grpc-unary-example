using Grpc.Net.Client;
using GrpcServer;
namespace GrpcClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Creating gRPC client");
            using var channel = GrpcChannel.ForAddress("http://localhost:5191");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest { Name = "Franco" });
            Console.WriteLine("Greeting: " + reply.Message);

            var sum = await client.AddNumbersAsync(new AddRequest { NumA = 4, NumB = 7 });
            Console.WriteLine("Sum: " + sum.Sum);

            UserListReply res = await client.GetUserListAsync(new Google.Protobuf.WellKnownTypes.Empty());
            foreach (User user in res.Users)
            {
                await Console.Out.WriteLineAsync($"{user.Name} tiene {user.Age} años");
            }
            if (res.HasMaxAge)
                Console.WriteLine($"The max age was set to {res.MaxAge}");
            else
                Console.WriteLine($"The max age was not set");
            Console.ReadLine();
        }
    }
}
