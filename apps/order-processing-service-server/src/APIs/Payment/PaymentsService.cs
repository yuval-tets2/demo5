using OrderProcessingService.Infrastructure;

namespace OrderProcessingService.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(OrderProcessingServiceDbContext context)
        : base(context) { }
}
