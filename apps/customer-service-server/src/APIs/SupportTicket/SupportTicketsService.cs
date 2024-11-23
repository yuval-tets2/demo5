using CustomerService.Infrastructure;

namespace CustomerService.APIs;

public class SupportTicketsService : SupportTicketsServiceBase
{
    public SupportTicketsService(CustomerServiceDbContext context)
        : base(context) { }
}
