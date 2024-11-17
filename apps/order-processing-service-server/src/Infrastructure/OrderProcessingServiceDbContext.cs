using Microsoft.EntityFrameworkCore;
using OrderProcessingService.Infrastructure.Models;

namespace OrderProcessingService.Infrastructure;

public class OrderProcessingServiceDbContext : DbContext
{
    public OrderProcessingServiceDbContext(
        DbContextOptions<OrderProcessingServiceDbContext> options
    )
        : base(options) { }

    public DbSet<OrderItemDbModel> OrderItems { get; set; }

    public DbSet<ShipmentDbModel> Shipments { get; set; }

    public DbSet<OrderDbModel> Orders { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }
}
