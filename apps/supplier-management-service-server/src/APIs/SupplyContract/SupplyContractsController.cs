using Microsoft.AspNetCore.Mvc;

namespace SupplierManagementService.APIs;

[ApiController()]
public class SupplyContractsController : SupplyContractsControllerBase
{
    public SupplyContractsController(ISupplyContractsService service)
        : base(service) { }
}
