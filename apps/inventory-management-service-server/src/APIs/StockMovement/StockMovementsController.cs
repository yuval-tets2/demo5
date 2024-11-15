using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementService.APIs;

[ApiController()]
public class StockMovementsController : StockMovementsControllerBase
{
    public StockMovementsController(IStockMovementsService service)
        : base(service) { }
}
