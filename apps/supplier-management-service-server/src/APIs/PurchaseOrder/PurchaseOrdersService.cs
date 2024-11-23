using SupplierManagementService.Infrastructure;

namespace SupplierManagementService.APIs;

public class PurchaseOrdersService : PurchaseOrdersServiceBase
{
    public PurchaseOrdersService(SupplierManagementServiceDbContext context)
        : base(context) { }
}
