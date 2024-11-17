using Microsoft.AspNetCore.Mvc;

namespace OrderProcessingService.APIs;

[ApiController()]
public class OrderItemsController : OrderItemsControllerBase
{
    public OrderItemsController(IOrderItemsService service)
        : base(service) { }
}
