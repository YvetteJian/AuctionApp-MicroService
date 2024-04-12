using Contracts;
using MassTransit;

namespace AuctionService;
//if throw exception, then it will go to the fault queue,and it have a consumer to deal withe the errors.
public class AuctionCreatedFaultConsumer : IConsumer<Fault<AuctionCreated>>
{
    public async Task Consume(ConsumeContext<Fault<AuctionCreated>> context)
    {
        Console.WriteLine("---->Consuming faulty creations");
        var exception = context.Message.Exceptions.First();
        if (exception.ExceptionType == "System.ArgumentException")
        {
            context.Message.Message.Model = "foobar";
        }
    }
}
