using Microsoft.AspNetCore.Mvc;

namespace OrderProcessingService.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
