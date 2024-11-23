using Microsoft.AspNetCore.Mvc;

namespace CustomerService.APIs;

[ApiController()]
public class InteractionsController : InteractionsControllerBase
{
    public InteractionsController(IInteractionsService service)
        : base(service) { }
}
