using SupplierManagementService.Infrastructure;

namespace SupplierManagementService.APIs;

public class DeliveriesService : DeliveriesServiceBase
{
    public DeliveriesService(SupplierManagementServiceDbContext context)
        : base(context) { }
}
