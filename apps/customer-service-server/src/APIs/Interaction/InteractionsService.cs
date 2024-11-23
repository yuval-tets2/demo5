using CustomerService.Infrastructure;

namespace CustomerService.APIs;

public class InteractionsService : InteractionsServiceBase
{
    public InteractionsService(CustomerServiceDbContext context)
        : base(context) { }
}
