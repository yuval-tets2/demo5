using SupplierManagementService.Infrastructure;

namespace SupplierManagementService.APIs;

public class SuppliersService : SuppliersServiceBase
{
    public SuppliersService(SupplierManagementServiceDbContext context)
        : base(context) { }
}
