using InventoryManagementService.Infrastructure;

namespace InventoryManagementService.APIs;

public class StockMovementsService : StockMovementsServiceBase
{
    public StockMovementsService(InventoryManagementServiceDbContext context)
        : base(context) { }
}
