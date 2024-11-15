using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementService.APIs;

[ApiController()]
public class WarehousesController : WarehousesControllerBase
{
    public WarehousesController(IWarehousesService service)
        : base(service) { }
}
