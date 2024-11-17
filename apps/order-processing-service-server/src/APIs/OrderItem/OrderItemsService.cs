using OrderProcessingService.Infrastructure;

namespace OrderProcessingService.APIs;

public class OrderItemsService : OrderItemsServiceBase
{
    public OrderItemsService(OrderProcessingServiceDbContext context)
        : base(context) { }
}
