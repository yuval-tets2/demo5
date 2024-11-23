using CustomerService.Infrastructure;

namespace CustomerService.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(CustomerServiceDbContext context)
        : base(context) { }
}
