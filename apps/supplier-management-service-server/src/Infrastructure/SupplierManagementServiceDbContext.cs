using Microsoft.EntityFrameworkCore;
using SupplierManagementService.Infrastructure.Models;

namespace SupplierManagementService.Infrastructure;

public class SupplierManagementServiceDbContext : DbContext
{
    public SupplierManagementServiceDbContext(
        DbContextOptions<SupplierManagementServiceDbContext> options
    )
        : base(options) { }

    public DbSet<SupplierDbModel> Suppliers { get; set; }

    public DbSet<DeliveryDbModel> Deliveries { get; set; }

    public DbSet<SupplyContractDbModel> SupplyContracts { get; set; }

    public DbSet<PurchaseOrderDbModel> PurchaseOrders { get; set; }
}
