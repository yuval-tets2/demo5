using ServiceFromPrivateTemplate.Infrastructure;

namespace ServiceFromPrivateTemplate.APIs;

public class FffsService : FffsServiceBase
{
    public FffsService(ServiceFromPrivateTemplateDbContext context)
        : base(context) { }
}
