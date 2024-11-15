using InventoryManagementService.Infrastructure;

namespace InventoryManagementService.APIs;

public class ProductsService : ProductsServiceBase
{
    public ProductsService(InventoryManagementServiceDbContext context)
        : base(context) { }
}
