using Microsoft.AspNetCore.Mvc;

namespace OrderProcessingService.APIs;

[ApiController()]
public class ShipmentsController : ShipmentsControllerBase
{
    public ShipmentsController(IShipmentsService service)
        : base(service) { }
}
