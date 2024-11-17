using Microsoft.AspNetCore.Mvc;

namespace ServiceFromPrivateTemplate.APIs;

[ApiController()]
public class FffsController : FffsControllerBase
{
    public FffsController(IFffsService service)
        : base(service) { }
}
