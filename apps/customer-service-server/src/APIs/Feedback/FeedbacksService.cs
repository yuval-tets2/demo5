using CustomerService.Infrastructure;

namespace CustomerService.APIs;

public class FeedbacksService : FeedbacksServiceBase
{
    public FeedbacksService(CustomerServiceDbContext context)
        : base(context) { }
}
