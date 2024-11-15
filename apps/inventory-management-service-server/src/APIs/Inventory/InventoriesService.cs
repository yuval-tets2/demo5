using InventoryManagementService.Infrastructure;

namespace InventoryManagementService.APIs;

public class InventoriesService : InventoriesServiceBase
{
    public InventoriesService(InventoryManagementServiceDbContext context)
        : base(context) { }
}
