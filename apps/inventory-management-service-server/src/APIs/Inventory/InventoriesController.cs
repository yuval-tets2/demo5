using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementService.APIs;

[ApiController()]
public class InventoriesController : InventoriesControllerBase
{
    public InventoriesController(IInventoriesService service)
        : base(service) { }
}
