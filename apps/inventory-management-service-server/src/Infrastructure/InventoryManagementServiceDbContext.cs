using InventoryManagementService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementService.Infrastructure;

public class InventoryManagementServiceDbContext : DbContext
{
    public InventoryManagementServiceDbContext(
        DbContextOptions<InventoryManagementServiceDbContext> options
    )
        : base(options) { }

    public DbSet<ProductDbModel> Products { get; set; }

    public DbSet<InventoryDbModel> Inventories { get; set; }

    public DbSet<StockMovementDbModel> StockMovements { get; set; }

    public DbSet<WarehouseDbModel> Warehouses { get; set; }
}
