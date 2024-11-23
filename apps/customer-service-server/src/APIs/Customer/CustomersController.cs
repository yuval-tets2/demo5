using Microsoft.AspNetCore.Mvc;

namespace CustomerService.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
