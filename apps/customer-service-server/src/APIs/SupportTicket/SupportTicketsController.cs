using Microsoft.AspNetCore.Mvc;

namespace CustomerService.APIs;

[ApiController()]
public class SupportTicketsController : SupportTicketsControllerBase
{
    public SupportTicketsController(ISupportTicketsService service)
        : base(service) { }
}
