using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementService.APIs;

[ApiController()]
public class ProductsController : ProductsControllerBase
{
    public ProductsController(IProductsService service)
        : base(service) { }
}
