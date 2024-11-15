using Microsoft.EntityFrameworkCore;

namespace ServiceFromPrivateTemplate.Infrastructure;

public class ServiceFromPrivateTemplateDbContext : DbContext
{
    public ServiceFromPrivateTemplateDbContext(
        DbContextOptions<ServiceFromPrivateTemplateDbContext> options
    )
        : base(options) { }
}
