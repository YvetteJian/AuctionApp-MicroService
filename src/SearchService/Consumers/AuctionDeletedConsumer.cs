using Contracts;
using DnsClient.Protocol;
using MassTransit;
using MongoDB.Entities;

namespace SearchService;

public class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
{
    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
        Console.WriteLine("--> Consuming auctionDeleted: " + context.Message.Id);
        var result = await DB.DeleteAsync<Item>(context.Message.Id);
        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(AuctionUpdated), "Problem deleting mongdo");
        }
    }

}
