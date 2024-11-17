using OrderProcessingService.Infrastructure;

namespace OrderProcessingService.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(OrderProcessingServiceDbContext context)
        : base(context) { }
}
