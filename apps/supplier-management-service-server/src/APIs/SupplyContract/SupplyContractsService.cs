using SupplierManagementService.Infrastructure;

namespace SupplierManagementService.APIs;

public class SupplyContractsService : SupplyContractsServiceBase
{
    public SupplyContractsService(SupplierManagementServiceDbContext context)
        : base(context) { }
}
