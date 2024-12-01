using InventoryManagementService.Infrastructure;

namespace InventoryManagementService.APIs;

public class WarehousesService : WarehousesServiceBase
{
    public WarehousesService(InventoryManagementServiceDbContext context)
        : base(context) { }
}
