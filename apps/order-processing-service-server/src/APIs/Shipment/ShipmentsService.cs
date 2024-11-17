using OrderProcessingService.Infrastructure;

namespace OrderProcessingService.APIs;

public class ShipmentsService : ShipmentsServiceBase
{
    public ShipmentsService(OrderProcessingServiceDbContext context)
        : base(context) { }
}
