using Microsoft.AspNetCore.Mvc;

namespace SupplierManagementService.APIs;

[ApiController()]
public class PurchaseOrdersController : PurchaseOrdersControllerBase
{
    public PurchaseOrdersController(IPurchaseOrdersService service)
        : base(service) { }
}
