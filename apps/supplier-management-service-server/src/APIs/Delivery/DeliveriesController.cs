using Microsoft.AspNetCore.Mvc;

namespace SupplierManagementService.APIs;

[ApiController()]
public class DeliveriesController : DeliveriesControllerBase
{
    public DeliveriesController(IDeliveriesService service)
        : base(service) { }
}
