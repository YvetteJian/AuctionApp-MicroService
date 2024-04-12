using Microsoft.EntityFrameworkCore;
using AuctionService.Entities;
using MassTransit;
namespace AuctionService.Data;


public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Auction> Auctions { get; set; }

    //for outbox functionality(if rabbitmq is down, put the publish msg in the out box)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}